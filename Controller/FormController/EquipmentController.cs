using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using WooJinDataBase;

namespace Controller
{

    public class EquipmentController
    {
        DataBase dataBase = DataBase.Instance;
        public EquipmentController()
        {
            
        }

        public object GetListEquipment()
        {
            return dataBase.GetListEquipment();
        }
    }
}
