using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using PowerClub.Bussiness.Utils;
using PowerClub.Bussiness.Model;
using PowerClub.DataAccess.Domain;


namespace PowerClub.Bussiness.Services
{
    public class BranchOfficeServices : BaseServices
    {

        //private readonly IGenericFactory<BranchOffice, int> fBranchOfficeFactoryAsync;

        //GYMEntities fcontext = new GYMEntities();
        public BranchOfficeServices()
        {
            //fBranchOfficeFactoryAsync = fGymUnitOfWork.CreateFactory<BranchOffice, int>();
        }

        public int Add(BranchOfficeModel aModel)
        {
            int result = 0;
            //await Task.Run(() =>
            //{
                try
                {
                    BranchOffice aNew = new BranchOffice
                    {
                        Code = aModel.Code,
                        Name = aModel.Name,
                        Address = aModel.Adress,
                        Observation = aModel.Observation,
                        Active = true,
                        StarHour = aModel.StarHour,
                        EndHour = aModel.EndHour,
                        DateCreated = DateTime.Now
                    };
                    fcontext.BranchOffice.Add(aNew);
                    fcontext.SaveChanges();
                    //fBranchOfficeFactoryAsync.Save(aNew);
                    //fGymUnitOfWork.Commit();
                    result = aNew.Id;
                }
                catch (Exception ex)
                {
                    result =0;
                }
            //});
            return result;
        }

        public bool Update(BranchOfficeModel aModel)
        {
            bool result = false;
            //await Task.Run(() =>
            //{
            try
            {
                var getToUpdate = fcontext.BranchOffice.First(a => a.Id == aModel.Id);
                //fBranchOfficeFactoryAsync.GetById(aModel.Id);
                getToUpdate.Code = aModel.Code;
                getToUpdate.Name = aModel.Name;
                getToUpdate.Address = aModel.Adress;
                getToUpdate.Observation = aModel.Observation;
                getToUpdate.Active = aModel.Active;
                getToUpdate.StarHour = aModel.StarHour;
                getToUpdate.EndHour = aModel.EndHour;
                getToUpdate.DateUpdated = aModel.DateUpdated;

                fcontext.Entry(getToUpdate).State = System.Data.Entity.EntityState.Modified;
                fcontext.SaveChanges();
                return true;

                //fBranchOfficeFactoryAsync.Update(getToUpdate);
                //fGymUnitOfWork.Commit();
                //result = true;

            }
            catch (Exception ex)
            {
                result = false;
            }
            //});
            return result;
        }

        public BranchOfficeModel GetByID(int Id)
        {
            BranchOfficeModel branchOffice = new BranchOfficeModel();
            try
            {
                var _branchOffice = fcontext.BranchOffice.First(a => a.Id == Id); // fBranchOfficeFactoryAsync.GetById(Id);
                branchOffice.Id = _branchOffice.Id;
                branchOffice.Code = _branchOffice.Code;
                branchOffice.Name = _branchOffice.Name;
                branchOffice.Adress = _branchOffice.Address;
                branchOffice.Observation = _branchOffice.Observation;
                branchOffice.Active = _branchOffice.Active;
                branchOffice.StarHour = _branchOffice.StarHour;
                branchOffice.EndHour = _branchOffice.EndHour;
                branchOffice.DateCreated = _branchOffice.DateCreated;
                branchOffice.DateUpdated = _branchOffice.DateUpdated;
                branchOffice.StringStartHour = branchOffice.StarHour?.ToString().Remove(branchOffice.StarHour.ToString().Length - 3) ?? string.Empty;
                branchOffice.StringEndHour = branchOffice.EndHour?.ToString().Remove(branchOffice.EndHour.ToString().Length - 3) ?? string.Empty;

            }
            catch (Exception ex)
            {
                branchOffice = null;
            }
            return branchOffice;
        }

        public ObservableCollection<BranchOfficeModel> GetAll()
        {
            var data = new ObservableCollection<BranchOfficeModel>();
            List<BranchOfficeModel> _list = new List<BranchOfficeModel>();
            try
            {

                var result = fcontext.BranchOffice.OrderBy(a => a.Name).ToList(); // fBranchOfficeFactoryAsync.GetAll().OrderBy(a => a.Name).ToList();

                foreach (var item in result)
                {
                    data.Add(new BranchOfficeModel
                    {
                        Id = item.Id,
                        Code = item.Code,
                        Name = item.Name,
                        Adress = item.Address,
                        Observation = item.Observation,
                        Active = item.Active,
                        IsActive = (item.Active == true) ? "Si" : "No",
                        StarHour = item.StarHour,
                        EndHour = item.EndHour,
                        StringStartHour  =  item.StarHour?.ToString().Remove(item.StarHour.ToString().Length - 3) ?? string.Empty,
                        StringEndHour = item.EndHour?.ToString().Remove(item.EndHour.ToString().Length - 3) ?? string.Empty,
                        DateCreated = item.DateCreated,
                        DateUpdated = item.DateUpdated
                    });
                }
                return new ObservableCollection<BranchOfficeModel>(data);
            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public bool DeleteUpdate(int Id)
        {
            bool result = false;
            BranchOfficeModel aModel = new BranchOfficeModel();
            try
            {
                BranchOffice branchOffice = fcontext.BranchOffice.Find(Id);
                fcontext.BranchOffice.Remove(branchOffice);
                fcontext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                aModel = GetByID(Id);
                aModel.Active = false;

                result = Update(aModel); //Update Change Estatus Active
            }
            return result;
        }

        public BranchOfficeModel GetByCode(string code)
        {
            BranchOfficeModel branchOffice = new BranchOfficeModel();
            try
            {
                var _branchOffice = fcontext.BranchOffice.First(a => a.Code == code.Trim()); // fBranchOfficeFactoryAsync.GetById(Id);
                branchOffice.Id = _branchOffice.Id;
                branchOffice.Name = _branchOffice.Name;
                branchOffice.Adress = _branchOffice.Address;
                branchOffice.Observation = _branchOffice.Observation;
                branchOffice.Active = _branchOffice.Active;
                branchOffice.StarHour = _branchOffice.StarHour;
                branchOffice.EndHour = _branchOffice.EndHour;
                branchOffice.DateCreated = _branchOffice.DateCreated;
                branchOffice.DateUpdated = _branchOffice.DateUpdated;
                branchOffice.StringStartHour = branchOffice.StarHour?.ToString().Remove(branchOffice.StarHour.ToString().Length - 3) ?? string.Empty;
                branchOffice.StringEndHour = branchOffice.EndHour?.ToString().Remove(branchOffice.EndHour.ToString().Length - 3) ?? string.Empty;

            }
            catch (Exception ex)
            {
                branchOffice = null;
            }
            return branchOffice;
        }
    }
}
