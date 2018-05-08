using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using PowerClub.Bussiness.Utils;
using PowerClub.Bussiness.Model;
using PowerClub.DataAccess.Domain;
//using PowerClub.DataAccess.Utils;

namespace PowerClub.Bussiness.Services
{
    public class TrainerServices : BaseServices
    {
    

        //private readonly IGenericFactory<Trainer, int> fTrainerFactoryAsync;

        GYMEntities context = new GYMEntities();
        public TrainerServices()
        {
            //fTrainerFactoryAsync = fGymUnitOfWork.CreateFactory<Trainer, int>();

        }

        public int Add(TrainerModel aModel)
        {
            int result = 0;
            //await Task.Run(() =>
            //{
                try
                {
                    Trainer aNew = new Trainer
                    {
                        Name = aModel.Name,
                        Code = aModel.Code,
                        Description = aModel.Description,
                        IdType = aModel.Type,
                        Active = true,
                        DateCreated = DateTime.Now,
                        Finger1 = aModel.Huella1,
                        Finger2 = aModel.Huella2,
                        Price = aModel.Price,
                        PhotoLink = aModel.PhotoLink
                    };
                    context.Trainer.Add(aNew);
                    context.SaveChanges();
                    result = aNew.Id;
                }
                catch (Exception ex)
                {
                    result = 0;
                }
            //});
            return result;
        }

        public bool Update(TrainerModel aModel)
        {
            bool result = false;
            //await Task.Run(() =>
            //{
            try
            {
                var getToUpdate = context.Trainer.First(a => a.Id == aModel.Id); //.GetById(aModel.Id);
                getToUpdate.Code = aModel.Code;
                getToUpdate.Name = aModel.Name;
                getToUpdate.Description = aModel.Description;
                getToUpdate.IdType = aModel.Type;
                getToUpdate.Active = aModel.Active;
                getToUpdate.DateUpdated = aModel.DateUpdated;
                getToUpdate.Finger1 = aModel.Huella1;
                getToUpdate.Finger2 = aModel.Huella2;
                getToUpdate.Price = aModel.Price;
                getToUpdate.PhotoLink = aModel.PhotoLink;
                context.Entry(getToUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                result = false;
            }
            //});
            return result;
        }

        public TrainerModel GetByID(int Id)
        {
            TrainerModel trainer = new TrainerModel();
            try
            {
                var _trainer = context.Trainer.First(a => a.Id == Id);// fTrainerFactoryAsync.GetById(Id);
                trainer.Id = _trainer.Id;
                trainer.Code = _trainer.Code;
                trainer.Name = _trainer.Name;
                trainer.Description = _trainer.Description;
                trainer.Type = _trainer.IdType;
                trainer.Active = _trainer.Active;
                trainer.DateCreated = _trainer.DateCreated;
                trainer.DateUpdated = _trainer.DateUpdated;
                trainer.Huella1 = _trainer.Finger1;
                trainer.Huella2 = _trainer.Finger2;
                trainer.PhotoLink = _trainer.PhotoLink;
                trainer.Price = _trainer.Price;
            }
            catch (Exception ex)
            {
                trainer = null;
            }
            return trainer;
        }

        public ObservableCollection<TrainerModel> GetAll()
        {
            var data = new ObservableCollection<TrainerModel>();
            try
            {
                var result = context.Trainer.OrderBy(a => a.Name).Select(a => new TrainerModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Code = a.Code,
                    Description = a.Description,
                    Type = a.IdType,
                    Active = a.Active,
                    IsActive = (a.Active== true)?"Si":"No",
                    NameType = a.TrainerType.Name,
                    DateCreated = a.DateCreated,
                    DateUpdated = a.DateUpdated,
                    Huella1 = a.Finger1,
                    IsHuella = (a.Finger1 != null)? "Si" : "No",
                    Price = a.Price,
                    PhotoLink = a.PhotoLink
                }).ToList();

                return new ObservableCollection<TrainerModel>(result);
            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public Dictionary<int, string> GetBTrainerType()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            try
            {
                dictionary = (from bo in context.TrainerType
                              select new { Id = bo.Id, NameTrainer = bo.Name }).ToDictionary(g => g.Id, g => g.NameTrainer);

            }
            catch (Exception ex)
            {
                dictionary = null;
            }
            return dictionary;
        }

        public bool DeleteUpdate(int Id)
        {
            bool result = false;
            TrainerModel aModel = new TrainerModel();
            try
            {
                Trainer trainer = fcontext.Trainer.Find(Id);
                fcontext.Trainer.Remove(trainer);
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

        public TrainerModel GetByCode(string code)
        {
            TrainerModel trainer = new TrainerModel();
            try
            {
                var _trainer = context.Trainer.First(a => a.Code == code.Trim());// fTrainerFactoryAsync.GetById(Id);
                trainer.Id = _trainer.Id;
                trainer.Code = _trainer.Code;
                trainer.Name = _trainer.Name;
                trainer.Description = _trainer.Description;
                trainer.Type = _trainer.IdType;
                trainer.Active = _trainer.Active;
                trainer.DateCreated = _trainer.DateCreated;
                trainer.DateUpdated = _trainer.DateUpdated;
                trainer.Huella1 = _trainer.Finger1;
                trainer.Huella2 = _trainer.Finger2;
                trainer.PhotoLink = _trainer.PhotoLink;
                trainer.Price = _trainer.Price;
            }
            catch (Exception ex)
            {
                trainer = null;
            }
            return trainer;
        }
    }
}
