using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAL
{
    class ProductCategory
    {
        private OnlineShopDbContext db = null;

        public ProductCategory()
        {
            db=new OnlineShopDbContext();
        }

        public int SynChronizedData(string data)
        {
            return 1;
        }
    }
}
