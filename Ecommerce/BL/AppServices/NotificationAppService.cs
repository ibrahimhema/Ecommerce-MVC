using BL.Bases;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
  public  class NotificationAppService:BaseAppService
    {
        #region CURD

        public List<Notifiication> GetAllNotifiication()
        {
            return TheUnitOfWork.Notification.GetAllNotification().ToList();
        }
        public int GetNotifiicationCount()
        {
            return TheUnitOfWork.Notification.GetAllNotification().Count(); 
        }
        public Notifiication GetNotifiication(int id)
        {
            return TheUnitOfWork.Notification.GetNotifiicationById(id);
        }



        public bool SaveNewNotifiication(Notifiication notifiication)
        {
            bool result = false;
      
            if (TheUnitOfWork.Notification.InsertNotifiication(notifiication))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateNotifiication(Notifiication notifiication)
        {
         
            TheUnitOfWork.Notification.UpdateNotifiication(notifiication);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteNotification(int id)
        {
            TheUnitOfWork.Notification.DeleteNotifiication(id);
            bool result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckNotificationExists(Notifiication notifiication)
        {
           
            return TheUnitOfWork.Notification.CheckNotifiicationExists(notifiication);
        }
        #endregion
    }
}
