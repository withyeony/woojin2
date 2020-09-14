using System;
using Controller;
using Model;
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
    public partial class Equipment_Maintenance : Form, IBasicForm
    {
        private BasicForm _Mother;
        private Member _LoginInfo;
        private EquipmentController _equipment;


        DataTable table = new DataTable();

        public Equipment_Maintenance(Member member, BasicForm form)
        {

            InitializeComponent();
            _Mother = form;
            _LoginInfo = member;
            _equipment = new EquipmentController();

            GetListEquipment();

            //InsertData();
        }

        private void GetListEquipment()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;

            dataGridView1.Rows.Clear();
            List<Equipment> equipments = new List<Equipment>();
            equipments = (List<Equipment>)_equipment.GetListEquipment();
            if (equipments is null)
            {
                ;
            }
            else
            {
                foreach (Equipment equipment in equipments)
                {
                    dataGridView1.Rows.Add(equipment.No, equipment.Name, equipment.Supervisor, equipment.Useable, equipment.day, equipment.Check, equipment.Inspector);

                }
            }
        }

        /*private void InsertData()
{
   List<Equipment> list = _equipment.GetList();
   if (list is null)
   {

   }
   else
   {
       foreach (Machine2 mac in list)
       {
           if (mac.점검상태.Equals("완료"))
           {
               dataGridView2.Rows.Add(mac.No, mac.장비명, mac.담당자, mac.사용유무, mac.설비보전일자, mac.점검상태, mac.검사자);
           }
           else
           {
               dataGridView1.Rows.Add(mac.No, mac.장비명, mac.담당자, mac.사용유무, mac.설비보전일자, mac.점검상태, mac.검사자);
           }
       }
   }
}*/


        public Form SetForm()
        {
            return this;
        }
    }
}