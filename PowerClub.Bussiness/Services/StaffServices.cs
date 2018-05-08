using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Utils;
using PowerClub.DataAccess.Domain;


//using PowerClub.DataAccess.Utils;

namespace PowerClub.Bussiness.Services
{
    public class StaffServices : BaseServices
    {
        //private readonly IGenericFactory<Staff, int> fStaffFactoryAsync;

        //GYMEntities fcontext = new GYMEntities();
        public StaffServices()
        {
            // fStaffFactoryAsync = fGymUnitOfWork.CreateFactory<Staff, int>();
        }

        public ObservableCollection<StaffModel> GetAll()
        {
            List<StaffModel> List = new List<StaffModel>();
            try
            {
                List = fcontext.Staff.Select(a => new StaffModel()
                {
                    Id = a.Id,
                    Code = a.Code,
                    Name = a.Name,                   
                    Active = a.Active,
                    DateCreated = a.DateCreated,
                    DateUpdated = a.DateUpdated,
                    Huella = (a.Finger1 != null),
                    IsHuella = (a.Finger1 != null)? "Si":"No",
                    IsActive= ((bool)a.Active) ? "Si" : "No",
                    Huella1 = a.Finger1,
                    Huella2 = a.Finger2
                }).ToList();

                return new ObservableCollection<StaffModel>(List);

            }
            catch (Exception ex)
            {
                return new ObservableCollection<StaffModel>(List);
            }
        }

        public StaffModel GetById(int Id)
        {
            StaffModel staff = new StaffModel();
            try
            {
                var result = fcontext.Staff.First(a => a.Id == Id);
                staff.Id = result.Id;
                staff.Code = result.Code;
                staff.Name = result.Name;
                staff.Active = result.Active;
                staff.DateCreated = result.DateCreated;
                staff.DateUpdated = result.DateUpdated;
                staff.Huella1 = result.Finger1;
                staff.Huella2 = result.Finger2;

            }
            catch (Exception ex)
            {
                staff = null;
            }
            return staff;
        }

        public int Add(StaffModel aModel)
        {
            int result = 0;
            try
            {
                //await Task.Run(() =>
                //{
                Staff aNew = new Staff()
                {
                    Code = aModel.Code,
                    Name = aModel.Name,
                    Active = aModel.Active,
                    DateCreated = DateTime.Now,
                    Finger1 = aModel.Huella1,
                    Finger2 = aModel.Huella2
                };
                fcontext.Staff.Add(aNew);
                fcontext.SaveChanges();
                result = aNew.Id;
                //});
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }

        public bool Update(StaffModel aModel)
        {
            bool result = false;
            try
            {
                var getforUpdated = fcontext.Staff.First(a => a.Id == aModel.Id);
                // fStaffFactoryAsync.GetById(aModel.Id);
                getforUpdated.Code = aModel.Code;
                getforUpdated.Name = aModel.Name;
                getforUpdated.Active = aModel.Active;
                getforUpdated.DateUpdated =DateTime.Now;
                getforUpdated.Finger1 = aModel.Huella1;
                getforUpdated.Finger2 = aModel.Huella2;

                fcontext.Entry(getforUpdated).State = System.Data.Entity.EntityState.Modified;
                fcontext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool DeleteUpdate(int Id)
        {
            bool result = false;
            StaffModel aModel = new StaffModel();
            try
            {
                Staff staff = fcontext.Staff.Find(Id);
                fcontext.Staff.Remove(staff);
                fcontext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                aModel = GetById(Id);
                aModel.Active = false;

                result = Update(aModel); //Update Change Estatus Active
            }
            return result;
        }

    }
}

