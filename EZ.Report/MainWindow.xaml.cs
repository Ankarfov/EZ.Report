using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

// Добавить в настройки звук при создании снимка

namespace EZ.Report
{
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]

        public static extern short GetAsyncKeyState(int vKey);

        int ReportListCounter = 0;

        bool State = false;

		public bool ShotSound { get; set; } = false;

		Settings Win_Settings;

		public bool Win_Settings_Status { get; set; } = false;

		List<Report> ListOfReports = new List<Report>();

		Thread KeyInputThread;

		Thread KeyActionThread;

		BinaryFormatter formatter = new BinaryFormatter();

		public MainWindow()
        {
            InitializeComponent();

			try
            {
				using (var fileStream = new FileStream("EZ.Report.Settings", FileMode.Open))
				{
					ListOfReports = (List<Report>)formatter.Deserialize(fileStream);

					ShotSound = (bool)formatter.Deserialize(fileStream);
				}
			}
			catch
            {

            }

            if (ListOfReports.Count == 0) ListOfReports.Add(new Report());

            RefreshCounter(ReportListCounter, ListOfReports.Count);

            RefreshState(State);

            RefreshContent();

            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Button)
                {
                    ((Button)element).Click += Button_Click;
                }

                if (element is TextBlock)
                {
					((TextBlock)element).MouseLeftButtonDown += TextBlock_Mouse_LB_Down;
				}

                if (element is TextBox)
                {
					((TextBox)element).TextChanged += TextBox_Text_Changed;
				}
            }
        }

        // Действия с GUI

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Button = (string)((Button)e.OriginalSource).Name;

            switch (Button)
            {
                case "AddBTN":

                    if (ListOfReports.Count != 99)
                    {
                        ListOfReports.Add(new Report());

                        RefreshCounter(ReportListCounter, ListOfReports.Count);
                    }

                    RefreshContent();

                    break;

                case "DeleteBTN":

                    if (ListOfReports.Count - 1 != 0)
                    {
                        ListOfReports.RemoveAt(ReportListCounter);

                        if (ReportListCounter >= ListOfReports.Count - 1) ReportListCounter = ListOfReports.Count - 1;

                        RefreshCounter(ReportListCounter, ListOfReports.Count);
                    }

                    RefreshContent();

                    break;

                case "LeftBTN":

                    if(ReportListCounter != 0) ReportListCounter--;

                    RefreshCounter(ReportListCounter, ListOfReports.Count);

                    RefreshContent();

					if (KeyInputThread != null) if (KeyInputThread.ThreadState == ThreadState.Background) KeyInputThread.Abort();

					break;

                case "RightBTN":

                    if (ReportListCounter != ListOfReports.Count - 1) ReportListCounter++;

                    RefreshCounter(ReportListCounter, ListOfReports.Count);

                    RefreshContent();

					if (KeyInputThread != null) if (KeyInputThread.ThreadState == ThreadState.Background) KeyInputThread.Abort();

					break;

                case "SettingsBTN":

					if (Win_Settings_Status == false)
					{
						Win_Settings = new Settings();

						InitSettings();

						Win_Settings_Status = true;

						Win_Settings.Show();
					}

					break;

                case "StateBTN":

                    if (State)
                    {
                        State = false;

						KeyActionThread.Abort();
					}
                    else
                    {
                        State = true;

						KeyActionThread = new Thread(GetKeyCombo) { IsBackground = true };

						KeyActionThread.Priority = ThreadPriority.Highest;

						KeyActionThread.Start();
					}

                    RefreshState(State);

                    break;
            }
        }

        private void TextBlock_Mouse_LB_Down(object sender, RoutedEventArgs e)
        {
			string TextBlock = (string)((TextBlock)e.OriginalSource).Name;

			switch (TextBlock)
			{
				case "PathTB":

					Keyboard.ClearFocus();

					SaveFileDialog SaveDialog = new SaveFileDialog();

					SaveDialog.Filter = "JPEG|*.jpg";

					SaveDialog.FileName = NameTB.Text;

					Nullable<bool> ResultSave = SaveDialog.ShowDialog();

					if (ResultSave == true)
					{
						string FilePath = SaveDialog.FileName;

						FilePath = PathUpdate(FilePath, NameTB.Text);

						ListOfReports[ReportListCounter].Path = FilePath;

						RefreshContent();
					}

					break;

				case "KeyTB":

					Keyboard.ClearFocus();

					if (!State)
                    {
						if (KeyInputThread == null)
						{
							KeyInputThread = new Thread(SetKeyCombo) { IsBackground = true };

							KeyInputThread.Start();

							KeyTB.Text = ". . .";
						}
						else
						{
							if (KeyInputThread.ThreadState != ThreadState.Background)
							{
								KeyInputThread = new Thread(SetKeyCombo) { IsBackground = true };

								KeyInputThread.Start();

								KeyTB.Text = ". . .";
							}
						}
					}

					break;
			}
		}

        private void TextBox_Text_Changed(object sender, RoutedEventArgs e)
        {
            string TextBox = (string)((TextBox)e.OriginalSource).Name;

           switch (TextBox)
            {
                case "NameTB":

                    ListOfReports[ReportListCounter].Name = NameTB.Text;

                    RefreshContent();

                    break;
            }
        }

		// Обработка комбинаций клавиш

		public void GetKeyCombo()
		{
			List<int> KeyBoardState = new List<int>();

			bool TurnOFF = false;

			bool Consist = false;

			int Getch_KeyBoardState = 0;

			int Getch_KeyCombo = 0;

			List<int> ToReturn = new List<int>();

			bool NotReady = true;

			while (NotReady)
			{
				NotReady = false;

				Thread.Sleep(50);

				for (int i = -300; i < 300; i++)
				{
					Getch_KeyBoardState = 0;

					if (Math.Abs(Convert.ToInt32(GetAsyncKeyState(i))) > 0) Getch_KeyBoardState = Math.Abs(Convert.ToInt32(GetAsyncKeyState(i)));

					if (Getch_KeyBoardState > 0 && i != 0 && i != 1 && i != 2 && i != 4 && i != 5 && i != 6)
					{
						NotReady = true;
					}
				}
			}

			while (true)
			{
				KeyBoardState.Clear();

				Thread.Sleep(50);

				for (int i = -300; i < 300; i++)
				{
					Getch_KeyBoardState = 0;

					if (Math.Abs(Convert.ToInt32(GetAsyncKeyState(i))) > 0) Getch_KeyBoardState = Math.Abs(Convert.ToInt32(GetAsyncKeyState(i)));

					if (Getch_KeyBoardState > 0 && i != 0 && i != 1 && i != 2 && i != 4 && i != 5 && i != 6)
					{
						KeyBoardState.Add(i);
					}
				}

				if (KeyBoardState.Count() > 0)
				{
					TurnOFF = true;
				}

				if (KeyBoardState.Count() > 0)
				{
					for (int i = -300; i < 300; i++)
					{
						Getch_KeyCombo = 0;

						if (Math.Abs(Convert.ToInt32(GetAsyncKeyState(i))) > 0) Getch_KeyCombo = Math.Abs(Convert.ToInt32(GetAsyncKeyState(i)));

						if (Getch_KeyCombo > 0)
						{
							for (int n = 0; n < ToReturn.Count(); n++)
							{
								if (ToReturn[n] == i)
								{
									Consist = true;
								}
							}

							if (Consist == false && i != 0 && i != 1 && i != 2 && i != 4 && i != 5 && i != 6)
							{
								ToReturn.Add(i);
							}

							Consist = false;
						}
					}
				}

				if (TurnOFF == true && KeyBoardState.Count() == 0)
				{
					if (ToReturn.Count >= 1)
					{
						ToReturn = CheckedKeyCombo(SortKeyCombo(ToReturn));

						if (ToReturn[0] <= -200)
                        {
							int SelectedReport = ToReturn[0] + 300;

							PhotoMaker(SelectedReport);
						}

						ToReturn.Clear();
					}
				}
			}
		}

		public void SetKeyCombo()
        {
			ListOfReports[ReportListCounter].Key.Clear();

			List<int> KeyBoardState = new List<int>();

            bool TurnOFF = false;

            bool Consist = false;

            int Getch_KeyBoardState = 0;

            int Getch_KeyCombo = 0;

            List<int> ToReturn = new List<int>();

            bool NotReady = true;

            while (NotReady)
            {
                NotReady = false;

                for (int i = -300; i < 300; i++)
                {
                    Getch_KeyBoardState = 0;

                    if (Math.Abs(Convert.ToInt32(GetAsyncKeyState(i))) > 0) Getch_KeyBoardState = Math.Abs(Convert.ToInt32(GetAsyncKeyState(i)));

                    if (Getch_KeyBoardState > 0 && i != 0 && i != 1 && i != 2 && i != 4 && i != 5 && i != 6)
                    {
                        NotReady = true;
                    }
                }
            }

            while (true)
            {
                KeyBoardState.Clear();

                for (int i = -300; i < 300; i++)
                {
                    Getch_KeyBoardState = 0;

                    if (Math.Abs(Convert.ToInt32(GetAsyncKeyState(i))) > 0) Getch_KeyBoardState = Math.Abs(Convert.ToInt32(GetAsyncKeyState(i)));

                    if (Getch_KeyBoardState > 0 && i != 0 && i != 1 && i != 2 && i != 4 && i != 5 && i != 6)
                    {
                        KeyBoardState.Add(i);
                    }
                }

                if (KeyBoardState.Count() > 0)
                {
                    TurnOFF = true;
                }

                if (KeyBoardState.Count() > 0)
                {
                    for (int i = -300; i < 300; i++)
                    {
                        Getch_KeyCombo = 0;

                        if (Math.Abs(Convert.ToInt32(GetAsyncKeyState(i))) > 0) Getch_KeyCombo = Math.Abs(Convert.ToInt32(GetAsyncKeyState(i)));

                        if (Getch_KeyCombo > 0)
                        {
                            for (int n = 0; n < ToReturn.Count(); n++)
                            {
                                if (ToReturn[n] == i)
                                {
                                    Consist = true;
                                }
                            }

                            if (Consist == false && i != 0 && i != 1 && i != 2 && i != 4 && i != 5 && i != 6)
                            {
                                ToReturn.Add(i);
                            }

                            Consist = false;
                        }
                    }
                }

                if (TurnOFF == true && KeyBoardState.Count() == 0)
                {
                    if (ToReturn.Count >= 1)
                    {
                        ListOfReports[ReportListCounter].Key = CheckedKeyCombo(SortKeyCombo(ToReturn));
					}

					if (KeyInputThread != null)
                    {
						if (KeyInputThread.ThreadState == ThreadState.Background)
						{
							this.Dispatcher.Invoke(() =>
							{
								RefreshKeyCombo();
							});

							KeyInputThread.Abort();
						}
					}				
				}
            }
        }

        List<int> SortKeyCombo(List<int> KeyCombo)
        {
            for (int i = 0; i < KeyCombo.Count(); i++)
            {
                if (KeyCombo[i] == 16)
                {
                    KeyCombo.Remove(KeyCombo[i]);
                }

                if (KeyCombo[i] == 17)
                {
                    KeyCombo.Remove(KeyCombo[i]);
                }

                if (KeyCombo[i] == 18)
                {
                    KeyCombo.Remove(KeyCombo[i]);
                }

                if (KeyCombo[i] == 255)
                {
                    KeyCombo.Remove(KeyCombo[i]);
                }
            }

            KeyCombo.Sort();

            return KeyCombo;
        }

		string KeyTranslate(int Key)
		{
			string KeyName = "Key : #" + Key;

			char Slash = Convert.ToChar(92);

			switch (Key)
			{
				case 27:

					KeyName = "Esc";

					break;
				case 112:

					KeyName = "F1";

					break;
				case 113:

					KeyName = "F2";

					break;
				case 114:

					KeyName = "F3";

					break;
				case 115:

					KeyName = "F4";

					break;
				case 116:

					KeyName = "F5";

					break;
				case 117:

					KeyName = "F6";

					break;
				case 118:

					KeyName = "F7";

					break;
				case 119:

					KeyName = "F8";

					break;
				case 120:

					KeyName = "F9";

					break;
				case 121:

					KeyName = "F10";

					break;
				case 122:

					KeyName = "F11";

					break;
				case 123:

					KeyName = "F12";

					break;
				case 192:

					KeyName = "`";

					break;
				case 49:

					KeyName = "1";

					break;
				case 50:

					KeyName = "2";

					break;
				case 51:

					KeyName = "3";

					break;
				case 52:

					KeyName = "4";

					break;
				case 53:

					KeyName = "5";

					break;
				case 54:

					KeyName = "6";

					break;
				case 55:

					KeyName = "7";

					break;
				case 56:

					KeyName = "8";

					break;
				case 57:

					KeyName = "9";

					break;
				case 48:

					KeyName = "0";

					break;
				case 189:

					KeyName = "-";

					break;
				case 187:

					KeyName = "=";

					break;
				case 8:

					KeyName = "Backspace";

					break;
				case 9:

					KeyName = "Tab";

					break;
				case 81:

					KeyName = "Q";

					break;
				case 87:

					KeyName = "W";

					break;
				case 69:

					KeyName = "E";

					break;
				case 82:

					KeyName = "R";

					break;
				case 84:

					KeyName = "T";

					break;
				case 89:

					KeyName = "Y";

					break;
				case 85:

					KeyName = "U";

					break;
				case 73:

					KeyName = "I";

					break;
				case 79:

					KeyName = "O";

					break;
				case 80:

					KeyName = "P";

					break;
				case 219:

					KeyName = "[";

					break;
				case 221:

					KeyName = "]";

					break;
				case 220:

					KeyName = Slash.ToString();

					break;
				case 20:

					KeyName = "CapsLock";

					break;
				case 65:

					KeyName = "A";

					break;
				case 83:

					KeyName = "S";

					break;
				case 68:

					KeyName = "D";

					break;
				case 70:

					KeyName = "F";

					break;
				case 71:

					KeyName = "G";

					break;
				case 72:

					KeyName = "H";

					break;
				case 74:

					KeyName = "J";

					break;
				case 75:

					KeyName = "K";

					break;
				case 76:

					KeyName = "L";

					break;
				case 186:

					KeyName = ";";

					break;
				case 222:

					KeyName = "'";

					break;
				case 13:

					KeyName = "Enter";

					break;
				case 160:

					KeyName = "Left Shift";

					break;
				case 90:

					KeyName = "Z";

					break;
				case 88:

					KeyName = "X";

					break;
				case 67:

					KeyName = "C";

					break;
				case 86:

					KeyName = "V";

					break;
				case 66:

					KeyName = "B";

					break;
				case 78:

					KeyName = "N";

					break;
				case 77:

					KeyName = "M";

					break;
				case 188:

					KeyName = ",";

					break;
				case 190:

					KeyName = ".";

					break;
				case 191:

					KeyName = "/";

					break;
				case 161:

					KeyName = "Right Shift";

					break;
				case 162:

					KeyName = "Left Ctrl";

					break;
				case 91:

					KeyName = "Left Win";

					break;
				case 164:

					KeyName = "Left Alt";

					break;
				case 32:

					KeyName = "Space";

					break;
				case 165:

					KeyName = "Right Alt";

					break;
				case 92:

					KeyName = "Right Win";

					break;
				case 93:

					KeyName = "Cont. Menu";

					break;
				case 163:

					KeyName = "Right Ctrl";

					break;
				case 45:

					KeyName = "Insert";

					break;
				case 36:

					KeyName = "Home";

					break;
				case 33:

					KeyName = "PageUp";

					break;
				case 46:

					KeyName = "Delete";

					break;
				case 35:

					KeyName = "End";

					break;
				case 34:

					KeyName = "PageDown";

					break;
				case 38:

					KeyName = "Up";

					break;
				case 40:

					KeyName = "Down";

					break;
				case 37:

					KeyName = "Left";

					break;
				case 39:

					KeyName = "Right";

					break;
				case 44:

					KeyName = "Print Screen";

					break;
				case 145:

					KeyName = "Scr. Lock";

					break;
				case 144:

					KeyName = "NumLock";

					break;
				case 111:

					KeyName = "Num '/'";

					break;
				case 106:

					KeyName = "Num '*'";

					break;
				case 109:

					KeyName = "Num '-'";

					break;
				case 103:

					KeyName = "Num '7'";

					break;
				case 104:

					KeyName = "Num '8'";

					break;
				case 105:

					KeyName = "Num '9'";

					break;
				case 107:

					KeyName = "Num '+'";

					break;
				case 100:

					KeyName = "Num '4'";

					break;
				case 101:

					KeyName = "Num '5'";

					break;
				case 102:

					KeyName = "Num '6'";

					break;
				case 97:

					KeyName = "Num '1'";

					break;
				case 98:

					KeyName = "Num '2'";

					break;
				case 99:

					KeyName = "Num '3'";

					break;
				case 96:

					KeyName = "Num '0'";

					break;
				case 110:

					KeyName = "Num '.'";

					break;
				case 12:

					KeyName = "Num 'KP5'";

					break;
				case 1:

					KeyName = "LMB";

					break;
				case 2:

					KeyName = "RMB";

					break;
				case 4:

					KeyName = "MMB";

					break;
				case 5:

					KeyName = "AMB 1";

					break;
				case 6:

					KeyName = "AMB 2";

					break;
				case 255:

					KeyName = "Не назначено";

					break;
			}

			return KeyName;
		}

		List<int> CheckedKeyCombo(List<int> KeyCombo)
        {
			for (int n = 0; n < ListOfReports.Count; n++)
            {
				List<int> Result = ListOfReports[n].Key.Where(a => KeyCombo.Contains(a)).ToList();

				if (Result.Count == KeyCombo.Count && Result.Count == ListOfReports[n].Key.Count)
                {
					KeyCombo.Clear();

					KeyCombo.Add(n - 300);

					break;
				}
            }

			return KeyCombo;
		}

		// Создание фото

		private void PhotoMaker(int ReportNumber)
        {
			Bitmap BM = new Bitmap((int)System.Windows.SystemParameters.PrimaryScreenWidth, (int)System.Windows.SystemParameters.PrimaryScreenHeight);

			Graphics GH = Graphics.FromImage(BM as System.Drawing.Image);
			GH.CopyFromScreen(0, 0, 0, 0, BM.Size);

			string FinalPath = ListOfReports[ReportNumber].Path;

			DateTime LocalDate = DateTime.Now;

			FinalPath = FinalPath.Replace("*", LocalDate.ToString("MM.dd.yyyy.HH.mm.ss"));

			BM.Save(FinalPath, System.Drawing.Imaging.ImageFormat.Jpeg);

			GC.Collect();

			if (ShotSound) System.Media.SystemSounds.Beep.Play();
		}

		// Создание пути

		private string PathUpdate(string DefaultPath, string FolderName)
        {
			string UpdatedPath = "";

			List<string> PartsOfPath = DefaultPath.Split('\\').ToList();

			for (int n = 0; n < PartsOfPath.Count - 1; n++)
            {
				UpdatedPath += PartsOfPath[n] + "\\";
			}

			UpdatedPath += FolderName;

			if (!Directory.Exists(UpdatedPath))
			{
				DirectoryInfo di = Directory.CreateDirectory(UpdatedPath);
			}

			UpdatedPath += "\\" + "*" + "." + PartsOfPath[PartsOfPath.Count - 1].Split('.')[1];

			return UpdatedPath;
        }

		// Отображение пути

		private string MakeShortPath(string FullPath)
        {
            string ShortPath = "";

            string[] Elements = new string[FullPath.Split('\\').Length];

            Elements = FullPath.Split('\\');

            if (Elements.Length > 1)
            {
                ShortPath = Elements[0] + "\\" + ". . ." + "\\" + Elements[Elements.Length - 2].Split('.')[0] + "\\" + "*." + Elements[Elements.Length - 1].Split('.')[1];
            }
            else
            {
                ShortPath = FullPath;
            }

            return ShortPath;
        }

        // Обновление комбинации клавиш

        private void RefreshKeyCombo()
        {
            string KeyCombo = "";

            for (int n = 0; n < ListOfReports[ReportListCounter].Key.Count; n++)
            {
				KeyCombo += KeyTranslate(ListOfReports[ReportListCounter].Key[n]);

				if (n + 1 < ListOfReports[ReportListCounter].Key.Count)
                {
					KeyCombo += " + ";
				}
			}

			if (KeyCombo == "") KeyCombo = "Комбинация клавиш";

			if (KeyCombo.Contains("Key : #")) KeyCombo = "Ошибка";

			KeyTB.Text = KeyCombo;
        }

        // Обновление состояния

        private void RefreshState(bool State)
        {
            if (State)
            {
                StateBTN.Content = "Включено";

                StateBTN.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x00, 0xAA, 0x21));
            }
            else
            {
                StateBTN.Content = "Выключено";

                StateBTN.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0xFF, 0x30, 0x30));
            }
        }

        // Обновление содержимого

        private void RefreshContent()
        {
            NameTB.Text = ListOfReports[ReportListCounter].Name;

            PathTB.Text = MakeShortPath(ListOfReports[ReportListCounter].Path);

			RefreshKeyCombo();
		}

        // Обновление счетчика

        private void RefreshCounter(int Position, int Capacity)
        {
            Position += 1;

            CounterLeftTB.Text = Position.ToString();

            CounterRightTB.Text = Capacity.ToString();
        }

        // Действия с окном

        private void UpperBorder(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
			if (Win_Settings_Status == true) Win_Settings.Close();

			using (var fileStream = new FileStream("EZ.Report.Settings", FileMode.Create))
			{
				formatter.Serialize(fileStream, ListOfReports);

				formatter.Serialize(fileStream, ShotSound);
			}

			Close();
        }

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
			if (Win_Settings_Status == true) Win_Settings.Close();

			WindowState = WindowState.Minimized;
        }

		private void InitSettings()
		{
			Win_Settings.General = this;

			Win_Settings.ShotSound = ShotSound;

			Win_Settings.RefreshSoundButton(ShotSound);
		}

		// Пасхалка

		private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			System.Diagnostics.Process.Start(e.Uri.ToString());
		}
	}
}
