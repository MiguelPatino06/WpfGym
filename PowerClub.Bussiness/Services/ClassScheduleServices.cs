using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using PowerClub.Bussiness.Utils;
using PowerClub.Bussiness.Model;
using PowerClub.DataAccess.Domain;
//using PowerClub.DataAccess.Utils;

namespace PowerClub.Bussiness.Services
{
    public class ClassScheduleServices : BaseServices
    {
        //private readonly IGenericFactory<ClassSchedule, int> fClassScheduleFactoryAsync;
        private const string DefaultColor = "LightGreen";

        //GYMEntities fcontext = new GYMEntities();
        TrainerServices trainer = new TrainerServices();

        public ClassScheduleServices()
        {
           // fClassScheduleFactoryAsync = fGymUnitOfWork.CreateFactory<ClassSchedule, int>();
        }

        public int Add(ClassScheduleModel aModel)
        {
            int result = 0;
            //await Task.Run(() =>
            //{
                try
                {            
                    ClassSchedule aNew = new ClassSchedule
                    {
                        Id = aModel.Id,
                        IdBranchOficce = aModel.IdBranchOffice,
                        IdWorkout = aModel.IdWorkout,
                        DayWeek = aModel.WeekDay,
                        StarTime = aModel.StarTime,
                        EndTime = aModel.EndTime,
                        IdTrainer = aModel.IdTrainer== 0 ? null : aModel.IdTrainer,
                        Active = true,
                        DateCreated = DateTime.Now,
                        StartDate = aModel.StartDate,
                        EndDate = aModel.EndDate
                    };
                    fcontext.ClassSchedule.Add(aNew);
                    fcontext.SaveChanges();
                    result = aNew.Id;
                }
                catch (Exception ex)
                {
                    result = 0;
                }
            //});
            return result;
        }

        public bool Update(ClassScheduleModel aModel)
        {
            bool result = false;
            //await Task.Run(() =>
            //{
                try
                {
                    var getToUpdate = fcontext.ClassSchedule.First(a => a.Id == aModel.Id); // fClassScheduleFactoryAsync.GetById(aModel.Id);
                    getToUpdate.Id = aModel.Id;
                    getToUpdate.IdBranchOficce = aModel.IdBranchOffice;
                    getToUpdate.IdWorkout = aModel.IdWorkout;
                    getToUpdate.DayWeek = aModel.WeekDay;
                    getToUpdate.StarTime = aModel.StarTime;
                    getToUpdate.EndTime = aModel.EndTime;
                    getToUpdate.IdTrainer = aModel.IdTrainer;
                    getToUpdate.Active = aModel.Active;
                    getToUpdate.DateUdated = aModel.DateUpdated;
                    getToUpdate.StartDate = aModel.StartDate;
                    getToUpdate.EndDate = aModel.EndDate;

                    fcontext.Entry(getToUpdate).State = System.Data.Entity.EntityState.Modified;
                    fcontext.SaveChanges();
                //fClassScheduleFactoryAsync.Update(getToUpdate);
                //fGymUnitOfWork.Commit();
                result = true;

                }
                catch (Exception ex)
                {
                    result = false;
                }
            //});
            return result;
        }

        public List<ClassScheduleModel> GetAll()
        {
            List<ClassScheduleModel> _list = new List<ClassScheduleModel>();

            try
            {
                //fClassScheduleFactoryAsync.Find(new List<Expression<Func<ClassSchedule, object>>> { aUser => aUser.BranchOffice, aUser => aUser.Workout }, a => a.Active == true).Select(b => new ClassScheduleModel
                var _lista = fcontext.ClassSchedule.Where(a=> a.Active == true).Select(b => new ClassScheduleModel 
                {
                    Id = b.Id,
                    IdBranchOffice = b.IdBranchOficce,
                    IdWorkout = b.IdWorkout,
                    WeekDay = b.DayWeek,
                    StarTime = b.StarTime,
                    EndTime = b.EndTime,
                    IdTrainer = b.IdTrainer,
                    Active = b.Active,
                    DateCreated = b.DateCreated,
                    DateUpdated = b.DateUdated,
                    NameBranchOffice = b.BranchOffice.Name,
                    Subject = b.Workout.Name,
                    Color = b.Workout.Color ?? DefaultColor,
                    IdParent = b.Workout.Parent != null ? (int)b.Workout.Parent: b.Workout.Id,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate
                }).ToList();


                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    int day = DateTime.Now.Day;
                    if (day < 3) day = 3;
             
                DateTime _MondayOfWeek = StartOfWeek(DateTime.Now, DayOfWeek.Monday); // get Monday from week



                foreach (var item in _lista)
                {
                    if (item.Color == "") item.Color = null; 

                    int hs = 0, ms = 0, he = 0, me = 0;
                    string trainerName = string.Empty;

                    #region Validate Hour, idTrainer
                    if ((item.StarTime != null) || (item.EndTime != null))
                    {
                        hs = item.StarTime.Value.Hours;
                        ms = item.StarTime.Value.Minutes;
                        he = item.EndTime.Value.Hours;
                        me = item.EndTime.Value.Minutes;
                    }
                    if (!string.IsNullOrEmpty(item.IdTrainer.ToString()))
                    {
                        if(item.IdTrainer > 0)
                            trainerName = trainer.GetByID((int)item.IdTrainer).Name;
                    }
                    #endregion

                    string _scolor = item.Color;
                    //if (string.IsNullOrEmpty(item.IdTrainer.ToString()))
                    //    _scolor = "Aqua";

                    if (string.IsNullOrEmpty(item.Color))
                        _scolor = "Aqua";

                    _list.Add(new ClassScheduleModel
                    {
                        Id = item.Id,
                        IdBranchOffice = item.IdBranchOffice,
                        IdWorkout = item.IdWorkout,
                        IdParent = item.IdParent,
                        Subject = item.Subject,
                        Start = DateFormat(_MondayOfWeek, (int)item.WeekDay, hs, ms), // new DateTime(year, month, day - 1, hs, ms, 00),
                        End = DateFormat(_MondayOfWeek, (int)item.WeekDay, he, me),
                        TrainerName = trainerName,
                        BrushColor = new BrushConverter().ConvertFromString(_scolor ?? "Blue") as Brush,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate
                    });

                }
            }
            catch (Exception ex)
            {
                _list = null;
            }
            return _list;
        }

        public Dictionary<int, string> GetBranchOficefromClassSchedule(int id)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            try
            {
                dictionary = (from cs in fcontext.ClassSchedule
                    join bo in fcontext.BranchOffice on cs.IdBranchOficce equals bo.Id
                    where cs.IdWorkout == id && cs.Active == true
                 select new {Id = bo.Id, NameBranchOffice = bo.Name}).ToDictionary(g => g.Id, g => g.NameBranchOffice);

            }
            catch (Exception ex)
            {
                dictionary = null;
            }
            return dictionary;
        }

        public Dictionary<int, string> GetBranchOfice()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            try
            {
                dictionary = (from bo in fcontext.BranchOffice
                              where bo.Active == true
                              select new { Id = bo.Id, NameBranchOffice = bo.Name }).ToDictionary(g => g.Id, g => g.NameBranchOffice);

            }
            catch (Exception ex)
            {
                dictionary = null;
            }
            return dictionary;
        }

        public Dictionary<int, string> GetClassScheduleBranchOfice(int id)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            try
            {
                dictionary = (from wo in fcontext.WorkoutParentClassSchedule
                              where wo.IdBranchOficce == id
                              select new { Id = (int)wo.IdParent, NameWorkout = wo.ParentName }).Distinct().ToDictionary(g => g.Id, g => g.NameWorkout);

            }
            catch (Exception ex)
            {
                dictionary = null;
            }
            return dictionary;
        }

        public Dictionary<int, string> GetClassScheduleBranchOficeAndWorkout(int idBranch, int idWorkout)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            try
            {
                dictionary = (from wo in fcontext.WorkoutParentClassSchedule
                              where wo.IdBranchOficce == idBranch && wo.IdParent == idWorkout
                              select new { Id = (int)wo.Id, NameWorkout = wo.Name }).ToDictionary(g => g.Id, g => g.NameWorkout);

            }
            catch (Exception ex)
            {
                dictionary = null;
            }
            return dictionary;
        }


        public static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            DayOfWeek fdow = ci.DateTimeFormat.FirstDayOfWeek;
            return DateTime.Today.AddDays(-(DateTime.Today.DayOfWeek - fdow));
        }

        public static DateTime DateFormat(DateTime monday, int dayOfWeak, int hour, int min)
        {
            DateTime _monday = monday.AddDays(dayOfWeak-1); // add day to week begin Monday
            return _monday.AddHours(hour).AddMinutes(min).AddSeconds(0);
        }

        public ObservableCollection<ClassScheduleModel> GetList()
        {
            var data = new ObservableCollection<ClassScheduleModel>();
            try
            {
                CultureInfo ci = CultureInfo.InvariantCulture;
                var schedule = new List<ClassScheduleModel>();
                //var result =
                //    fClassScheduleFactoryAsync.Find(
                //        new List<Expression<Func<ClassSchedule, object>>>
                //        {
                //            aUser => aUser.BranchOffice,
                //            aUser => aUser.Workout
                //        }).OrderBy(a=> a.Workout.Name).ToList();



                var result =fcontext.ClassSchedule.OrderBy(a => a.Workout.Name).ToList();


                var _day = new DayofWeek();
              
                foreach (var item in result)
                {
                    string _nametrainer = null;

                    if (item.IdTrainer != null)
                    {
                        var result2 = trainer.GetByID((int) item.IdTrainer);
                        if (result2 != null)
                            _nametrainer = result2.Name;
                    }

                    schedule.Add(new ClassScheduleModel()
                    {
                        Id = item.Id,
                        IdBranchOffice = item.IdBranchOficce,
                        IdWorkout = item.IdWorkout,
                        Day = _day.GetAll().First(a => a.Day == (int)item.DayWeek).Name, // Common.GetNameDay((int) item.DayWeek),                       
                        StarTime = item.StarTime,
                        StartHour = item.StarTime.ToString().Remove(item.StarTime.ToString().Length - 3),
                        EndTime = item.EndTime,
                        EndHour = item.EndTime.ToString().Remove(item.EndTime.ToString().Length - 3),
                        Active = item.Active,
                        IsActive = (item.Active == true) ? "Si" : "No",
                        NameBranchOffice = item.BranchOffice.Name,
                        NameWorkout = item.Workout.Name,
                        TrainerName = _nametrainer, // item.IdTrainer != null?  trainer.GetByID((int) item.IdTrainer).Name: string.Empty,
                        DateCreated = item.DateCreated,
                        DateUpdated = item.DateUdated,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate
                    });
                }

                return new ObservableCollection<ClassScheduleModel>(schedule);

            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public ClassScheduleModel GetById(int Id)
        {
            var data = new ClassScheduleModel();
            try
            {
                string trainerName = string.Empty;
                TrainerModel _trainer = new TrainerModel();
                string _color = string.Empty;

                var _lista = fcontext.ClassSchedule.Where(a=> a.Id== Id).Select(b => new ClassScheduleModel
                {
                    Id = b.Id,
                    IdBranchOffice = b.IdBranchOficce,
                    IdWorkout = b.IdWorkout,
                    NameWorkout = b.Workout.Name,
                    WeekDay = b.DayWeek,
                    StarTime = b.StarTime,
                    EndTime = b.EndTime,
                    IdTrainer = b.IdTrainer,
                    Active = b.Active,
                    DateCreated = b.DateCreated,
                    DateUpdated = b.DateUdated,
                    NameBranchOffice = b.BranchOffice.Name,
                    Subject = b.Workout.Name,
                    Color = b.Workout.Color ?? DefaultColor,
                    IdParent = b.Workout.Parent != null ? (int)b.Workout.Parent : b.Workout.Id, 
                    StarBranchOffice  =  b.BranchOffice.StarHour,
                    EndBranchOffice = b.BranchOffice.EndHour,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate
                }).First();

                //var _lista =
                //    fClassScheduleFactoryAsync.Find(
                //        new List<Expression<Func<ClassSchedule, object>>>
                //        {
                //            aUser => aUser.BranchOffice,
                //            aUser => aUser.Workout
                //        }, a => a.Id == Id).Select(b => new ClassScheduleModel
                //        {
                //            Id = b.Id,
                //            IdBranchOffice = b.IdBranchOficce,
                //            IdWorkout = b.IdWorkout,
                //            NameWorkout = b.Workout.Name,
                //            WeekDay = b.DayWeek,
                //            StarTime = b.StarTime,
                //            EndTime = b.EndTime,
                //            IdTrainer = b.IdTrainer,
                //            Active = b.Active,
                //            DateCreated = b.DateCreated,
                //            DateUpdated = b.DateUdated,
                //            NameBranchOffice = b.BranchOffice.Name,
                //            Subject = b.Workout.Name,
                //            Color = b.Workout.Color ?? DefaultColor,
                //            IdParent = b.Workout.Parent != null ? (int)b.Workout.Parent : b.Workout.Id
                //        }).First();

                if (!string.IsNullOrEmpty(_lista.IdTrainer.ToString()))
                {
                    _trainer = trainer.GetByID((int)_lista.IdTrainer);
                }

                if (string.IsNullOrEmpty(_lista.Color))
                    _color = "Aqua";
                else
                    _color = _lista.Color;


                var _day = new DayofWeek();
                _lista.Day = _day.GetAll().First(a => a.Day == (int)_lista.WeekDay).Name;
                _lista.TrainerName = _trainer != null ? _trainer.Name: string.Empty;
                _lista.HuellaTrainer1 = _trainer != null ? _trainer.Huella1 : new byte[2];
                _lista.HuellaTrainer2 = _trainer != null ? _trainer.Huella2 : new byte[2];

                _lista.BrushColor = new BrushConverter().ConvertFromString(_color) as Brush;  //new BrushConverter().ConvertFromString(_lista.Color) as Brush;

                _lista.StartHour = _lista.StarTime.ToString().Remove(_lista.StarTime.ToString().Length - 3);
                _lista.EndHour = _lista.EndTime.ToString().Remove(_lista.EndTime.ToString().Length - 3);

                return  _lista;

            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public ObservableCollection<ClassScheduleModel> GetSearch(int IdB, int IdW, int IdT)
        {
            var data = new ObservableCollection<ClassScheduleModel>();
            try
            {
                CultureInfo ci = CultureInfo.InvariantCulture;
                var schedule = new List<ClassScheduleModel>();
                var result = fcontext.GetClassScheduleByOfficeWorkouTrainer1(IdB, IdW, IdT).OrderByDescending(a=> a.DateCreated).ToList();
               

                var _day = new DayofWeek();

                foreach (var item in result)
                {
                    schedule.Add(item: new ClassScheduleModel()
                    {
                        Id = item.Id,
                        IdBranchOffice = item.IdBranchOficce,
                        IdWorkout = item.IdWorkout,
                        Day = _day.GetAll().First(a => a.Day == (int)item.DayWeek).Name, // Common.GetNameDay((int) item.DayWeek),
                        StarTime = item.StarTime,
                        StartHour = item.StarTime.ToString().Remove(item.StarTime.ToString().Length - 3),
                        EndTime = item.EndTime,
                        EndHour = item.EndTime.ToString().Remove(item.EndTime.ToString().Length - 3),
                        Active = item.Active,
                        IsActive = (item.Active == true) ? "Si" : "No",
                        NameBranchOffice = item.NameBranchOffice,
                        NameWorkout = item.NameWorkout,
                        TrainerName = item.IdTrainer != null ? trainer.GetByID((int)item.IdTrainer).Name : string.Empty,
                        DateCreated = item.DateCreated,
                        DateUpdated = item.DateUdated,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate
                    });
                }

                return new ObservableCollection<ClassScheduleModel>(schedule);

            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public bool DeleteUpdate(int Id)
        {
            bool result = false;
            ClassScheduleModel aModel = new ClassScheduleModel();
            try
            {
                ClassSchedule classSchedule = fcontext.ClassSchedule.Find(Id);
                fcontext.ClassSchedule.Remove(classSchedule);
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
