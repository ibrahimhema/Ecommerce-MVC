using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Hlper
{
    public class helper
    {
        ApplicationDbContext context = new ApplicationDbContext();
        List<int> treeArr;
    public List<Sub_Category>showTree(int id)
        {
            treeArr = new List<int>();
            getAllLevel(id);
            List<Sub_Category> sub = new List<Sub_Category>();
                if (treeArr != null)
                {
                for (int i=0;i<treeArr.Count();i++)
                {
                    int idsub = treeArr[i];
                    sub.Add( context.Sub_Categories.Where(s => s.Id == idsub).FirstOrDefault());

                }

            }
       
            if (sub!=null)
            {
                return sub;
            }
            else
                return new List<Sub_Category>();



        }
        /*public void getAllLevel(int id)
        {
            //transalate lang depend on default langueges
            Sub_Category child = context.Sub_Categories.Where(s => s.Parent_Id == id).FirstOrDefault();

            if (child == null)
                return;
            id = child.Id;

            treeArr.Add(id);
            getAllLevel(id);
        }*/
        public void getAllLevel(int id)
        {
            //transalate lang depend on default langueges
         List<Sub_Category> childs= context.Sub_Categories.Where(s => s.Parent_Id == id).ToList();
            if (childs == null)
                return;
            foreach (var subs in childs)
            {
              
                id = subs.Id;
                treeArr.Add(id);
        
            }
         
      
          
        }
    }
}
