using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


public class FunFile
{
    public static string iniAppSchd = @".\resources\ini\AppSchd.ini";
    public static string iniDefault = @".\resources\ini\default.ini";
    private static int EvtStatus = 0;
    //private static int ErrStatus = 0;
    private static int DbgStatus = 0;
    private static string[] EvtLogBox = new string[] { };
    private static string[] ErrLogBox = new string[] { };
    private static string[] DbgLogBox = new string[] { };
    private static string EvtLog = GetString(iniDefault, "[LOG]", "EvtLogFolder")[0] + DateTime.Now.ToString("yyyyMMdd") + "evt.log";
    private static string ErrLog = GetString(iniDefault, "[LOG]", "ErrLogFolder")[0] + DateTime.Now.ToString("yyyyMMdd") + "err.log";
    private static string DbgLog = GetString(iniDefault, "[LOG]", "DbgLogFolder")[0] + DateTime.Now.ToString("yyyyMMdd") + "dbg.log";
    private static int ProcWaitSec = int.Parse(string.Format("{0}", GetString(iniDefault, "[LOG]", "ProcWaitSec")[0]));
    private static int iniEvtLogLevel = int.Parse(string.Format("{0}", GetString(iniDefault, "[LOG]", "SettingCode")[0])) + 1;
    private static int iniErrLogLevel = int.Parse(string.Format("{0}", GetString(iniDefault, "[LOG]", "SettingCode")[1])) + 1;
    public static string[] GetString(string fileName, string sectionName, string keyName)
    {
        string[] output = new string[] { "ないよ" };
        string line1;
        int numOfSection = -99;
        IEnumerable<string> lines = File.ReadLines(fileName);
        for (int i = 0; i < lines.Count(); i++)
        {
            line1 = lines.Skip(i).First();
            if (line1.IndexOf(";") >= 0 || line1 == "") { continue; }
            else
            {
                int findIndex1;
                string findKey;
                if (line1 == sectionName) { numOfSection = 1; }
                else if (numOfSection == 1)
                {
                    if (line1.Substring(0, 1) == "[") { break; }
                    findIndex1 = line1.IndexOf("=");
                    findKey = line1.Substring(0, findIndex1);
                    if (findKey == keyName)
                    {
                        string line2 = line1.Substring(findIndex1 + 1, line1.Length - findIndex1 - 1);
                        output = line2.Split('^');
                    }
                }
            }
        }
        if (output[0] == "ないよ")
        {
            Task ActiveTask = ErrEvtProc($"ないよ\n{sectionName}：\nkeyName：{keyName}", 1);
        }
        return output;
    }
    public static void BackupExec(string filename, string key, int backupGeneration)
    {
        if (File.Exists(filename))
        {
            for (int i = 0; i < backupGeneration + 1; i++)
            {
                string OldestFileNo = (backupGeneration).ToString("00");
                string OldestFile = $"{filename}{key}{OldestFileNo}";
                string LatestFile = $"{filename}{key}01";
                if (i == 0)
                {
                    if (!File.Exists(OldestFile)) { continue; }
                    File.Delete(OldestFile);
                    Task ActiveTask = EvtProc($"{OldestFile}を削除しました。", 2);
                }
                else if (i < backupGeneration)
                {
                    string CopySourceFileNo = (backupGeneration - i).ToString("00");
                    string CopySourceFile = $"{filename}{key}{CopySourceFileNo}";
                    if (!File.Exists(CopySourceFile)) { continue; }
                    string CopyDistinationFileNo = (backupGeneration - i + 1).ToString("00");
                    string CopyDistinationFile = $"{filename}{key}{CopyDistinationFileNo}";
                    File.Copy(CopySourceFile, CopyDistinationFile);
                    File.Delete(CopySourceFile);
                    Task ActiveTask = EvtProc($"{CopyDistinationFile}を作成しました。\n{CopySourceFile}を削除しました。", 2);
                }
                else if (i == backupGeneration)
                {
                    File.Copy(filename, LatestFile);
                    Task ActiveTask = EvtProc($"{LatestFile}を作成しました。", 2);
                }
            }
        }
        else
        {
            Task ActiveTask = ErrEvtProc($"{filename}がありませんでした", 1);
        }
    }
    private static void WrtMsg(string fileName, string msg)
    {
        Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
        StreamWriter writer = null;
        try
        {
            writer = new StreamWriter(fileName, true, sjisEnc);
            writer.WriteLine($"[Thread{Thread.CurrentThread.ManagedThreadId.ToString()}]{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}>>{msg}");
        }
        catch (Exception ex)
        {
            Task ActiveTask = ErrEvtProc(ex.Message, 1);
        }
        finally
        {
            if (EvtStatus == 1 || DbgStatus == 1)
            {
                writer.Close();
            }
        }
    }
    public static async Task EvtProc(string msg, int LogLevel)
    {
        bool EvtLogCHK = LogLevel < iniEvtLogLevel;
        if (!EvtLogCHK) { return; }
        Array.Resize(ref EvtLogBox, EvtLogBox.Length + 1);
        EvtLogBox[EvtLogBox.Length - 1] = msg;
        if (EvtStatus == 1) { return; }
        Task RunTask = Task.Run(() =>
        {
            EvtStatus = 1;
            Thread.Sleep(ProcWaitSec);
            while (EvtLogBox.Length > 0)
            {
                WrtMsg(EvtLog, EvtLogBox[0].ToString());
                EvtLogBox = EvtLogBox.Skip(1).ToArray();
            }
        });
        await RunTask;
        EvtStatus = 0;
        RunTask.Dispose();
    }
    public static async Task ErrEvtProc(string msg, int LogLevel)
    {
        bool ErrLogCHK = LogLevel < iniErrLogLevel;
        if (!ErrLogCHK) { return; }
        bool MsgBoxFLG = GetString(iniDefault, "[LOG]", "SettingCode")[3] == "1";
        if (MsgBoxFLG) { MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
        Array.Resize(ref EvtLogBox, EvtLogBox.Length + 1);
        EvtLogBox[EvtLogBox.Length - 1] = msg;
        if (EvtStatus == 1) { return; }
        Task RunTask = Task.Run(() =>
        {
            EvtStatus = 1;
            Thread.Sleep(ProcWaitSec);
            while (EvtLogBox.Length > 0)
            {
                WrtMsg(EvtLog, EvtLogBox[0].ToString());
                EvtLogBox = EvtLogBox.Skip(1).ToArray();
            }
        });
        await RunTask;
        EvtStatus = 0;
        RunTask.Dispose();
    }
    public static async Task DbgProc(string msg)
    {
        Array.Resize(ref DbgLogBox, DbgLogBox.Length + 1);
        DbgLogBox[DbgLogBox.Length - 1] = msg;
        if (DbgStatus == 1) { return; }
        Task RunTask = Task.Run(() =>
        {
            DbgStatus = 1;
            Thread.Sleep(ProcWaitSec);
            while (DbgLogBox.Length > 0)
            {
                WrtMsg(DbgLog, DbgLogBox[0].ToString());
                DbgLogBox = DbgLogBox.Skip(1).ToArray();
            }
        });
        await RunTask;
        DbgStatus = 0;
        RunTask.Dispose();
    }
}