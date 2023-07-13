using BL.Bases;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
   public class NotificationReposatory:BaseRepository<Notifiication>
    {
        public NotificationReposatory(DbContext db) : base(db)
        {
        }

        #region CRUB

        public IQueryable<Notifiication> GetAllNotification()
        {
            return GetAll();
        }

        public bool InsertNotifiication(Notifiication notifiication)
        {
            return Insert(notifiication);
        }
        public void UpdateNotifiication(Notifiication notifiication)
        {
            Update(notifiication);
        }
        public void DeleteNotifiication(int id)
        {
            Delete(id);
        }

        public bool CheckNotifiicationExists(Notifiication notifiication)
        {
            return GetAny(b => b.Id == notifiication.Id);
        }
        public Notifiication GetNotifiicationById(int id)
        {
            return GetFirstOrDefault(b => b.Id == id);
        }
        #endregion
    }
}
