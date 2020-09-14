using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
	public partial class MachineView : Form, IBasicForm
	{
		private Member _LoginInfo;
		private BasicForm _Mother;
		private Machine _SelectedData;
		private MachineController _SI;
		private Boolean _NoCheck;
		private Boolean _IdCheck;


		DataTable table = new DataTable();
		//private BasicForm basicForm;

		public MachineView(Member member, BasicForm form)
		{
			InitializeComponent();
			_Mother = form;
			_LoginInfo = member;
			_SelectedData = new Machine();
			_SI = new MachineController();
			//InsertData();
			this.dataGridView1.Font = new Font("Tahoma", 10, FontStyle.Regular);


			/*List<Machine> list = _SI.*/GetList();

			ComboBoxSet();
			_SelectedData.No = (int)dataGridView1.Rows[0].Cells[0].Value;
			_SelectedData.Name = dataGridView1.Rows[0].Cells[1].Value.ToString();
			_SelectedData.Division = dataGridView1.Rows[0].Cells[2].Value.ToString();
			_SelectedData.Supervisor = dataGridView1.Rows[0].Cells[3].Value.ToString();
			_SelectedData.Subordinate = dataGridView1.Rows[0].Cells[4].Value.ToString();
			_SelectedData.Manufact = dataGridView1.Rows[0].Cells[5].Value.ToString();
			_SelectedData.Buy = dataGridView1.Rows[0].Cells[6].Value.ToString();
			_SelectedData.IP = dataGridView1.Rows[0].Cells[7].Value.ToString();
			_SelectedData.Use = dataGridView1.Rows[0].Cells[8].Value.ToString();

			SetSelectData();

		}

		/*private void InsertData()
		{
			List<Machine> list = _SI.GetList();
			if (list is null)
			{

			}
			else
			{
				foreach (Machine m in list)
				{
					dataGridView1.Rows.Add(m.No, m.Name, m.Division, m.Supervisor, m.Subordinate, m.Manufact, m.Buy, m.IP, m.Use);

				}

			}
		}*/

		private void ComboBoxSet()
		{						
			cb_Add_Supervisor.Items.Clear();
			cb_Add_Subordinate.Items.Clear();
			cb_Add_Manufact.Items.Clear();
			cb_Add_Division.Items.Clear();
			cb_Update_Manufact.Items.Clear();
			cb_Update_Division.Items.Clear();
			cb_Update_Supervisor.Items.Clear();
			cb_Update_Subordinate.Items.Clear();



			List<String> list = _SI.GetDefualtMachine("brandofmachine");
			if (list != null)
			{
				foreach (string str in list) { cb_Add_Manufact.Items.Add(str); }
				foreach (string str in list) { cb_Update_Manufact.Items.Add(str); }
				list.Clear();
			}
			
			list = _SI.GetDefualtMachine("divisionofmachine");
			if (list != null)
			{
				foreach (string str in list) { cb_Add_Division.Items.Add(str); }
				foreach (string str in list) { cb_Update_Division.Items.Add(str); }
				list.Clear();
			}
			list = _SI.GetDefualtMachine("supervisorofmachine");
			if (list != null)
			{
				foreach (string str in list) { cb_Add_Supervisor.Items.Add(str); }
				foreach (string str in list) { cb_Update_Supervisor.Items.Add(str); }
				list.Clear();
			}
			list = _SI.GetDefualtMachine("subordinateofmachine");
			if (list != null)
			{
				foreach (string str in list) { cb_Add_Subordinate.Items.Add(str); }
				foreach (string str in list) { cb_Update_Subordinate.Items.Add(str); }
				list.Clear();
			}

		}

		public Form SetForm()
		{
			return this;
		}
		private void GetList()
		{
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;

			dataGridView1.Rows.Clear();
			List<Machine> machines = new List<Machine>();
			machines = _SI.GetList();
			if (machines is null)
			{
				;
			}
			else
			{
				foreach (Machine machine in machines)
				{
					dataGridView1.Rows.Add(machine.No, machine.Name, machine.Division, machine.Supervisor, machine.Subordinate, machine.Manufact, machine.Buy, machine.IP, machine.Use);

				}
			}
		}
		private void button_GiWon_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;

			cb_Add_Manufact.SelectedIndex = -1;
			cb_Add_Supervisor.SelectedIndex = -1;
			cb_Add_Division.SelectedIndex = -1;

			for (int i = 0; i < this.label_6.Controls.Count; i++)
			{
				if ((Button)label_6.Controls[i] == btn && label_6.Controls[i].BackColor == System.Drawing.SystemColors.Control)
				{


					label_6.Controls[i].BackColor = System.Drawing.SystemColors.GradientInactiveCaption;

					if (label_6.Controls[i] == btn_Add)
					{
						_IdCheck = false;
						_NoCheck = false;

						tb_Add_No.ReadOnly = false;
						//tb_Add_IP.ReadOnly = false;

						cb_Add_Division.Text = "";
						tb_Add_No.Text = "";
						cb_Add_Subordinate.Text = "";
						tb_Add_Name.Text = "";
						cb_Add_Supervisor.Text = "";
						tb_Add_IP.Text = "";

						dtp_Add_Buy.Value = DateTime.Now;


						SetSelectPanel(pnl_Add);
					}
					if (label_6.Controls[i] == btn_Update)
					{
						SetSelectPanel(pnl_Update);
						SetUpdateData();
					}
					if (label_6.Controls[i] == btn_Delete)
					{
						SetSelectPanel(pnl_Select);
						btn_Delete_Apply.Visible = true;
						lbl_DataSelect.Text = "Data Delete";
					}
				}
				else
				{
					label_6.Controls[i].BackColor = System.Drawing.SystemColors.Control;
				}
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			if (cb_SelectBox.SelectedIndex == -1)
			{
				
			}
			else
			{
				String value = cb_SelectBox.SelectedItem.ToString();

				string result = "";
				switch (value)
				{

					case "번호":
						result = "No";
						break;
					case "구분":
						result = "Division";
						break;
					case "장비명":
						result = "Name";
						break;
					case "관리자(정)":
						result = "Supervisor";
						break;
					case "관리자(부)":
						result = "Subordinate";
						break;
					case "브랜드명":
						result = "Manufact";
						break;
					case "구입일자":
						result = "Buy";
						break;
					case "IP":
						result = "IP";
						break;
					case "사용유무":
						result = "Useable";
						break;
				}
				dataGridView1.Rows.Clear();

				List<Machine> machines = new List<Machine>();
				machines = _SI.FindDataMachine(result, tb_SelectData.Text);
				if (machines is null)
				{
					;
				}
				else
				{
					foreach (Machine machine in machines)
					{
						dataGridView1.Rows.Add(machine.No, machine.Name, machine.Division, machine.Supervisor, machine.Subordinate, machine.Manufact, machine.Buy, machine.IP, machine.Use);
					}
					btn_Delete_Apply.Visible = false;
					lbl_DataSelect.Text = "Data Select";

					_SelectedData.No = (int)dataGridView1.Rows[0].Cells[0].Value;
					_SelectedData.Name = dataGridView1.Rows[0].Cells[1].Value.ToString();
					_SelectedData.Division = dataGridView1.Rows[0].Cells[2].Value.ToString();
					_SelectedData.Supervisor = dataGridView1.Rows[0].Cells[3].Value.ToString();
					_SelectedData.Subordinate = dataGridView1.Rows[0].Cells[4].Value.ToString();
					_SelectedData.Manufact = dataGridView1.Rows[0].Cells[5].Value.ToString();
					_SelectedData.Buy = dataGridView1.Rows[0].Cells[6].Value.ToString();
					_SelectedData.IP = dataGridView1.Rows[0].Cells[7].Value.ToString();
					_SelectedData.Use = dataGridView1.Rows[0].Cells[8].Value.ToString();

					SetSelectData();

					SetSelectPanel(pnl_Select);
				}
			}
		}

		private void tb_SelectData_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.button1_Click_1(sender, e);
			}
		}

		private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
		{
			btn_Delete_Apply.Visible = false;
			lbl_DataSelect.Text = "Data Select";
			DataGridView dgv = (DataGridView)sender;

			if (dgv.SelectedRows.Count != 0)
			{
				DataGridViewRow dr = dataGridView1.SelectedRows[0];

				if (dr.Cells[0].Value is null)
				{
					;
				}
				else
				{
					_SelectedData.No = (int)dr.Cells[0].Value;
					_SelectedData.Name = dr.Cells[1].Value.ToString();
					_SelectedData.Division = dr.Cells[2].Value.ToString();
					_SelectedData.Supervisor = dr.Cells[3].Value.ToString();
					_SelectedData.Subordinate = dr.Cells[4].Value.ToString();
					_SelectedData.Manufact = dr.Cells[5].Value.ToString();
					_SelectedData.Buy = dr.Cells[6].Value.ToString();
					_SelectedData.IP = dr.Cells[7].Value.ToString();
					_SelectedData.Use = dr.Cells[8].Value.ToString();
				}
			}
			SetSelectData();

			SetSelectPanel(pnl_Select);

			for (int i = 0; i < this.label_6.Controls.Count; i++)
			{
				label_6.Controls[i].BackColor = System.Drawing.SystemColors.Control;
			}
		}
		private void SetSelectData()
		{
			lbl_No.Text = _SelectedData.No.ToString();
			lbl_Division.Text = _SelectedData.Division;
			lbl_Name.Text = _SelectedData.Name;
			lbl_Supervisor.Text = _SelectedData.Supervisor;
			lbl_Subordinate.Text = _SelectedData.Subordinate;
			lbl_Manufact.Text = _SelectedData.Manufact;
			lbl_Buy.Text = _SelectedData.Buy;
			lbl_IP.Text = _SelectedData.IP;
			lbl_Useable.Text = _SelectedData.Use;
		}
		private void SetSelectPanel(Panel pnl)
		{
			pnl.BringToFront();

		}
		private void SetUpdateData()
		{
			tb_Update_No.Text = _SelectedData.No.ToString();
			cb_Update_Division.Text = _SelectedData.Division;
			tb_Update_Name.Text = _SelectedData.Name;
			cb_Update_Supervisor.Text = _SelectedData.Supervisor;
			cb_Update_Subordinate.Text = _SelectedData.Subordinate;
			cb_Update_Manufact.Text = _SelectedData.Manufact;
			dtp_Update_Buy.Text = _SelectedData.Buy;
			tb_Update_IP.Text = _SelectedData.IP;
			cb_Update_Useable.Text = _SelectedData.Use;
		}

		private void DataPanelClose(object sender, EventArgs e)
		{
			DataPanelReset(); ;
		}
		private void DataPanelReset()
		{
			SetSelectPanel(pnl_Select);

			btn_Delete_Apply.Visible = false;
			lbl_DataSelect.Text = "Data Select";
		}
		private void Reset()
		{
			tb_Update_No.Text =				"";
			cb_Update_Division.Text =		"";
			tb_Update_Name.Text =			"";
			cb_Update_Supervisor.Text =		"";
			cb_Update_Subordinate.Text =	"";
			cb_Update_Manufact.Text =		"";
			dtp_Update_Buy.Text =			"";
			tb_Update_IP.Text =				"";
			cb_Update_Useable.Text =		"";

			tb_Add_No.Text =			"";
			cb_Add_Division.Text =		"";
			tb_Add_Name.Text =			"";
			cb_Add_Supervisor.Text =	"";
			cb_Add_Subordinate.Text =	"";
			cb_Add_Manufact.Text =		"";
			dtp_Add_Buy.Text =			"";
			tb_Add_IP.Text =			"";
			cb_Add_Useable.Text =		"";


			lbl_No.Text =			"";
			lbl_Division.Text =		"";
			lbl_Name.Text =			"";
			lbl_Supervisor.Text =	"";
			lbl_Subordinate.Text =	"";
			lbl_Manufact.Text =		"";
			lbl_Buy.Text =			"";
			lbl_IP.Text =			"";
			lbl_Useable.Text =		"";

			_SelectedData.No = (int)dataGridView1.Rows[0].Cells[0].Value;
			_SelectedData.Name = dataGridView1.Rows[0].Cells[1].Value.ToString();
			_SelectedData.Division = dataGridView1.Rows[0].Cells[2].Value.ToString();
			_SelectedData.Supervisor = dataGridView1.Rows[0].Cells[3].Value.ToString();
			_SelectedData.Subordinate = dataGridView1.Rows[0].Cells[4].Value.ToString();
			_SelectedData.Manufact = dataGridView1.Rows[0].Cells[5].Value.ToString();
			_SelectedData.Buy = dataGridView1.Rows[0].Cells[6].Value.ToString();
			_SelectedData.IP = dataGridView1.Rows[0].Cells[7].Value.ToString();
			_SelectedData.Use = dataGridView1.Rows[0].Cells[8].Value.ToString();
		}

		private void SetAlarm(String str)
		{
			Alarm alarm = new Alarm(str);
			alarm.ShowDialog();
		}

		private void tb_Add_No_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
			{
				e.Handled = true;
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			GetList();

			ComboBoxSet();
			_SelectedData.No = (int)dataGridView1.Rows[0].Cells[0].Value;
			_SelectedData.Name = dataGridView1.Rows[0].Cells[1].Value.ToString();
			_SelectedData.Supervisor = dataGridView1.Rows[0].Cells[3].Value.ToString();
			_SelectedData.Subordinate = dataGridView1.Rows[0].Cells[4].Value.ToString();
			_SelectedData.Manufact = dataGridView1.Rows[0].Cells[5].Value.ToString();
			_SelectedData.Buy = dataGridView1.Rows[0].Cells[6].Value.ToString();
			_SelectedData.IP = dataGridView1.Rows[0].Cells[7].Value.ToString();
			_SelectedData.Use = dataGridView1.Rows[0].Cells[8].Value.ToString();
			SetSelectData();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			String str = btn.Tag.ToString();
			_Mother.RemoveTab(str);
		}


		private void btn_Add_Click(object sender, EventArgs e)
		{
			if (_NoCheck)

			{
				Machine machine = new Machine();
				machine.No = Convert.ToInt32(tb_Add_No.Text);
				machine.Division = cb_Add_Division.Text;
				machine.Name = tb_Add_Name.Text;
				machine.Supervisor = cb_Add_Supervisor.Text;
				machine.Subordinate = cb_Add_Subordinate.Text;
				machine.Manufact = cb_Add_Manufact.Text;
				machine.Buy = dtp_Add_Buy.Text;
				machine.IP = tb_Add_IP.Text;
				machine.Use = cb_Add_Useable.Text;


				_SI.MachineAdd(machine);

				GetList();
				Reset();
				DataPanelReset();
				SetAlarm("설비 정보 추가가 완료 되었습니다.");
			}
            else
            {
			SetAlarm("중복확인이 필요합니다.");
			}
		}

		private void btn_Update_Click(object sender, EventArgs e)
		{
			Machine machine = new Machine();

			machine.No = Convert.ToInt32(tb_Update_No.Text);
			machine.Division = cb_Update_Division.Text;
			machine.Name = tb_Update_Name.Text;
			machine.Supervisor = cb_Update_Supervisor.Text;
			machine.Subordinate = cb_Update_Subordinate.Text;
			machine.Manufact = cb_Update_Manufact.Text;
			machine.Buy = dtp_Update_Buy.Text;
			machine.IP = tb_Update_IP.Text;
			machine.Use = cb_Update_Useable.Text;


			_SI.UpdateMachine(machine);

			GetList();
			Reset();
			DataPanelReset();
			SetAlarm("설비 정보 수정이 완료 되었습니다.");
		}

		private void btn_Delete_Apply_Click(object sender, EventArgs e)
		{
			_SI.MachineDelete(_SelectedData.No);

			GetList();
			Reset();
			DataPanelReset();
			SetAlarm("설비 정보 삭제가 완료 되었습니다.");
		}
		private void button7_Click(object sender, EventArgs e)
		{
			if (tb_Add_No.Text.Length > 0)
			{
				_NoCheck = _SI.CheckMachine("No", tb_Add_No.Text);
				String str = "이미 존재하는 번호 입니다.";
				if (_NoCheck)
				{
					tb_Add_No.ReadOnly = true;
					str = "사용가능한 번호입니다.";
				}
				SetAlarm(str);
			}

		}
    }
}
