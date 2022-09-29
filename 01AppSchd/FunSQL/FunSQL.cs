using System;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;


public class FunSQL
{
    //定義
    private static string dataSource = FunFile.GetString(FunFile.iniAppSchd, "[DB]", "DataSource")[0];
    private static bool Level1 = int.Parse(string.Format(FunFile.GetString(FunFile.iniDefault, "[LOG]", "SettingCode")[2])) > 0;
    private static bool Level2 = int.Parse(string.Format(FunFile.GetString(FunFile.iniDefault, "[LOG]", "SettingCode")[2])) > 1;
    public static string[][] SQLSELECT(string sqlcode, string sql, string[] parameters, string[] values)
    {
        StringBuilder SQLLOG1 = null;
        if (Level1)
        {
            SQLLOG1 = new StringBuilder($"SQLCODE：{sqlcode}\n");
            if (Level2)
            {
                SQLLOG1.Append($"SQL：{sql}\n");
            }
        }
        string[][] output = new string[][] { };
        SQLiteConnection Conn = new SQLiteConnection($"DataSource={dataSource}");
        Conn.Open();
        SQLiteCommand cmd = new SQLiteCommand(sql, Conn);
        SQLiteDataReader reader = null;
        if (parameters.Length != 0)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (Level2)
                {
                    SQLLOG1.Append($"PARAMETER{i}：{parameters[i]}\n");
                    SQLLOG1.Append($"VALUE{i}：{values[i]}\n");
                }
                cmd.Parameters.Add(new SQLiteParameter(parameters[i], values[i]));
            }
        }
        try
        {
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string[] StringBox = new string[] { };
                for (int i1 = 0; i1 < reader.FieldCount; i1++)
                {
                    Array.Resize(ref StringBox, i1 + 1);
                    StringBox[i1] = reader.GetValue(i1).ToString();
                }
                int i2 = output.Length;
                Array.Resize(ref output, i2 + 1);
                output[i2] = StringBox;
            }
            if (Level1)
            {
                SQLLOG1.Append("RESULT：**SUCCESS**");
                Task ActiveTask1 = FunFile.DbgProc(SQLLOG1.ToString());
            }
        }
        catch (Exception ex)
        {
            SQLLOG1.Append($"RESULT：**ERROR**\n");
            SQLLOG1.Append(ex.Message.ToString());
            Task ActiveTask1 = FunFile.DbgProc(SQLLOG1.ToString());
            Task ActiveTask2 = FunFile.ErrEvtProc(ex.Message.ToString(), 1);
        }
        finally
        {
            cmd.Dispose();
            Conn.Close();
        }
        return output;
    }
    public static void SQLDML(string sqlcode, string sql, string[] parameters, string[] values)
    {
        StringBuilder SQLLOG2 = null;
        if (Level1)
        {
            SQLLOG2 = new StringBuilder($"SQLCODE：{sqlcode}\n");
            if (Level2)
            {
                SQLLOG2.Append($"SQL：{sql}\n");
            }
        }
        string[][] output = new string[][] { };
        SQLiteConnection Conn = new SQLiteConnection($"DataSource={dataSource}");
        Conn.Open();
        SQLiteCommand cmd = new SQLiteCommand(sql, Conn);
        if (parameters.Length != 0)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (Level2)
                {
                    SQLLOG2.Append($"PARAMETER{i}：{parameters[i]}\n");
                    SQLLOG2.Append($"VALUE{i}：{values[i]}\n");
                }
                cmd.Parameters.Add(new SQLiteParameter(parameters[i], values[i]));
            }
        }
        try
        {
            cmd.ExecuteNonQuery();
            if (Level1)
            {
                SQLLOG2.Append("RESULT：**SUCCESS**");
                Task ActiveTask1 = FunFile.DbgProc(SQLLOG2.ToString());
            }
        }
        catch (Exception ex)
        {
            SQLLOG2.Append($"RESULT：**ERROR**\n");
            SQLLOG2.Append(ex.Message.ToString());
            Task ActiveTask1 = FunFile.DbgProc(SQLLOG2.ToString());
            Task ActiveTask2 = FunFile.ErrEvtProc(ex.Message.ToString(), 1);
        }
        finally
        {
            cmd.Dispose();
            Conn.Close();
        }
    }
}