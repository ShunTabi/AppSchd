using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AppSchd
{
    public partial class AppSchd : Form
    {
        public AppSchd()
        {
            InitializeComponent();
        }
        class ThisApplicationSetup
        {
            private static Button MainActiveButton = null;
            private static string[] CreateButton_LoadFunctionName()
            {
                return FunFile.GetString(FunFile.iniAppSchd, "[MAIN]", "FunctionNames");
            }
            private static void CreateButton_LoadDll(Panel p,string FunctionName)
            {
                if (p.Controls.Count == 1)
                {
                    p.Controls[0].Dispose();
                }
                try
                {
                    Assembly ClassDLL = Assembly.LoadFrom($@".\{FunctionName}.dll");
                    dynamic inst = Activator.CreateInstance(ClassDLL.GetType($"{FunctionName}.{FunctionName}"));
                    inst.MainClassExec(p);
                }
                catch (Exception ex)
                {
                    Task ActiveTask3 = FunFile.ErrEvtProc(ex.ToString(), 0);
                }
            }
            private static void CreateButton(Panel p1, Panel p2)
            {
                string[] output = CreateButton_LoadFunctionName();
                for (int i = output.Length-1; 0 <= i; i--)
                {
                    Button b = new Button
                    {
                        Name = output[i],
                        Width = 125,
                        Dock = DockStyle.Left,
                        BackColor = Color.RoyalBlue,
                        ForeColor = Color.White,
                        Image = Image.FromFile($@".\resources\png\{output[i]}.png"),
                        Text = output[i],
                        TextImageRelation = TextImageRelation.ImageBeforeText,
                        Font = new Font( "", 8, FontStyle.Regular),
                        FlatStyle = FlatStyle.Flat,
                    };
                    b.FlatAppearance.BorderSize = 0;
                    b.MouseEnter += (sender, e) =>
                    {
                        b.BackColor = Color.LightSteelBlue;
                    };
                    b.MouseLeave += (sender, e) =>
                    {
                        b.BackColor = Color.RoyalBlue;
                    };
                    b.Click += (sender, e) =>
                    {
                        if(MainActiveButton != null)
                        {
                            MainActiveButton.Enabled = true;
                        }
                        Task ActiveTask = FunFile.EvtProc($"{b.Name} Clicked!", 1);
                        MainActiveButton = b;
                        MainActiveButton.Enabled = false;
                        CreateButton_LoadDll(p2, b.Name);
                    }; 
                    p1.Controls.Add(b); 
                    if(b.Name == "Schedule") 
                    {
                        MainActiveButton = b;
                        MainActiveButton.Enabled = false;
                    }
                }
            }
            public static void ThisApplicationSetupExec(Panel p1,Panel p2)
            {
                CreateButton(p1,p2);
                CreateButton_LoadDll(p2, "Schedule");
            }
        }
        class ThisApplicationStartup
        {
            public static void ThisApplicationStartupExec()
            {
                Task.Run(() =>
                {
                    int BackupGeneration = int.Parse(string.Format("{0}", FunFile.GetString(FunFile.iniAppSchd, "[DB]", "BackupGeneration")));
                    if (int.Parse(string.Format("{0}", FunFile.GetString(FunFile.iniAppSchd, "[DB]", "VaccumStatus"))) == 1)
                    {
                        Task ActiveTask = FunFile.EvtProc("DBの圧縮を開始します。", 2);
                        FunSQL.SQLDML("***Backup***", "VACUUM", new string[] { }, new string[] { });
                    }
                    if (BackupGeneration > 0)
                    {
                        Task ActiveTask = FunFile.EvtProc("DBバックアップ処理を開始します。", 2);
                        Task.Run(() =>
                        {
                            FunFile.BackupExec(FunFile.GetString(FunFile.iniAppSchd, "[DB]", "DataSource")[0], "_bk", BackupGeneration);
                        });
                    }
                });
            }
        }
        private void AppToDo_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicationSetupExec( p1, p2);
            ThisApplicationStartup.ThisApplicationStartupExec();
            this.Location = new Point(10, 10);
        }
    }
}
