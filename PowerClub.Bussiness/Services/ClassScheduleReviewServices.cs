using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Utils;
using PowerClub.DataAccess.Domain;
//using PowerClub.DataAccess.Utils;

namespace PowerClub.Bussiness.Services
{
    public class ClassScheduleReviewServices : BaseServices
    {
        //private readonly IGenericFactory<ReviewClassSchedule, int> fClassScheduleRevFactoryAsync;
        private readonly ClassScheduleServices classScheduleServices = new ClassScheduleServices();

        private GYMEntities context = new GYMEntities();
        public string Delay { get; set; }
        private const string  _OPEN = "ABIERTA";
        private const string _CLOSE = "CERRADA";
        private const string _CANCEL = "CANCELADA";
        public ClassScheduleReviewServices()
        {
            // fClassScheduleRevFactoryAsync = fGymUnitOfWork.CreateFactory<ReviewClassSchedule, int>();
        }


        public int Add(ClassScheduleReviewModel aModel)
        {
            int result = 0;
            //await Task.Run(() =>
            //{
            try
            {
                ReviewClassSchedule aNew = new ReviewClassSchedule
                {
                    IdClassSchedule = aModel.IdClassSchedule,
                    Observation = aModel.Observation,
                    IdStaff = aModel.IdStaff,
                    IsTrainer = aModel.IsTrainer,
                    IdTrainer = aModel.IdTrainer,
                    Members = aModel.Members,
                    Status = aModel.Estatus,
                    StartTime = aModel.Start,
                    EndTime = aModel.End,
                    DateCreated = DateTime.Now
                };
                context.ReviewClassSchedule.Add(aNew);
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

        public ObservableCollection<ClassScheduleReviewModel> GetAll()
        {
            var data = new ObservableCollection<ClassScheduleReviewModel>();
            string delay = Delay;
            
            try
            {
                var result = context.ReviewClassSchedule.OrderBy(a => a.Id).ToList();
                //fClassScheduleRevFactoryAsync.Find(
                //    new List<Expression<Func<ReviewClassSchedule, object>>>
                //    {
                //        aUser => aUser.ClassSchedule,
                //        aUser => aUser.Staff
                //    }).OrderBy(a => a.Id).ToList();


                var x = delay; // Time delay
                TimeSpan timeDelay = new TimeSpan();
                List<ClassScheduleReviewModel> schedule = new List<ClassScheduleReviewModel>();
                foreach (var item in result)
                {

                    if (item.ClassSchedule.StarTime != null)
                        timeDelay = (TimeSpan) item.ClassSchedule.StarTime + (TimeSpan.FromMinutes(int.Parse(x)));
                            //start hour + delay

                    var _start = item.StartTime.ToString().Remove(item.StartTime.ToString().Length - 3);
                    var _end = item.EndTime.ToString().Remove(item.EndTime.ToString().Length - 3);
                    schedule.Add(new ClassScheduleReviewModel
                    {
                        Id = item.Id,
                        IdClassSchedule = item.IdClassSchedule,
                        Observation = item.Observation,
                        IdStaff = item.IdStaff,
                        IsTrainer = (bool) item.IsTrainer,
                        IdTrainer = (int) item.IdTrainer,
                        Members = (int) item.Members,
                        StartString = _start,
                        EndString = _end,
                        Flag = (item.StartTime > timeDelay) ? "1" : "0", //IF FLAG = 1, THEN TIME DELAY
                        Schedule = classScheduleServices.GetById(item.IdClassSchedule)
                    });

                }

                return new ObservableCollection<ClassScheduleReviewModel>(schedule);

            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public ObservableCollection<ClassScheduleReviewModel> GetAllByParameter(int IdOffice, DateTime DSt, DateTime DEn)
        {
            var data = new List<ClassScheduleReviewModel>();

            string delay = Delay;
            string _status = string.Empty;
            try
            {
                var result = (IdOffice == 0)
                    ? context.ReviewClassSchedule.Where(a => a.DateCreated >= DSt && a.DateCreated <= DEn).ToList()
                    : context.ReviewClassSchedule.Where(
                        a => a.ClassSchedule.IdBranchOficce == IdOffice && (a.DateCreated >= DSt && a.DateCreated <= DEn)).ToList();

                var x = delay; // Time delay
                TimeSpan timeDelay = new TimeSpan();
                List<ClassScheduleReviewModel> schedule = new List<ClassScheduleReviewModel>();
                foreach (var item in result)
                {

                    if (item.ClassSchedule.StarTime != null)
                        timeDelay = (TimeSpan) item.ClassSchedule.StarTime + (TimeSpan.FromMinutes(int.Parse(x)));
                    //start hour + delay

                    var _start = item.StartTime.ToString().Remove(item.StartTime.ToString().Length - 3);
                    var _end =  item.EndTime?.ToString().Remove(item.EndTime.ToString().Length - 3) ?? string.Empty;

                    var getScheduleById = classScheduleServices.GetById(item.IdClassSchedule);

                    switch (item.Status)
                    {
                        case 1:
                            _status = _OPEN;
                            break;
                        case 2:
                            _status = _CLOSE;
                            break;
                        case 3:
                            _status = _CANCEL;
                            break;
                    }


                    schedule.Add(new ClassScheduleReviewModel
                    {
                        Id = item.Id,
                        IdClassSchedule = item.IdClassSchedule,
                        Observation = item.Observation,
                        IdStaff = item.IdStaff,
                        StaffName = item.Staff.Name,
                        EstatusName = _status, //(item.Status == 1)? "CERRADO": "CANCELADO",
                        IsTrainer = (bool) item.IsTrainer,
                        IdTrainer = (int) item.IdTrainer,
                        Members = (int) item.Members,
                        StartString = _start,
                        EndString = _end,
                        DateCreated = item.DateCreated,
                        Flag = (item.StartTime > timeDelay) ? "1" : "0", //IF FLAG = 1, THEN TIME DELAY
                        Schedule = getScheduleById, // classScheduleServices.GetById(item.IdClassSchedule),
                        Day = getScheduleById.Day + " " + item.DateCreated.ToString("dd/MM/yyy")
                    });

                }

                return new ObservableCollection<ClassScheduleReviewModel>(schedule);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ClassScheduleReviewModel GetByIdAndDate(int idClass)
        {
            try
            {
                DateTime dw = DateTime.Now;
               return fcontext.ReviewClassSchedule.Where(a => a.IdClassSchedule == idClass && 
                                                                 a.DateCreated.Year == dw.Year && 
                                                                 a.DateCreated.Month == dw.Month && 
                                                                 a.DateCreated.Day == dw.Day).Select(a => new ClassScheduleReviewModel
                {
                    Id = a.Id,
                    IdClassSchedule = a.IdClassSchedule,
                    Observation = a.Observation,
                    IdStaff = a.IdStaff,
                    IsTrainer = (bool)a.IsTrainer,
                    IdTrainer = (int)a.IdTrainer,
                    Members = (int)a.Members,
                    Start = (TimeSpan)a.StartTime,
                    End = (TimeSpan)a.EndTime,
                    Estatus = a.Status,
                    DateCreated = a.DateCreated
                }).First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public bool Update(ClassScheduleReviewModel aModel)
        {
            bool result = false;
            //await Task.Run(() =>
            //{
            try
            {
                var getToUpdate = fcontext.ReviewClassSchedule.First(a => a.Id == aModel.Id);

                getToUpdate.Observation = aModel.Observation;
                getToUpdate.IdStaff = aModel.IdStaff;
                getToUpdate.IsTrainer = aModel.IsTrainer;
                getToUpdate.Status = aModel.Estatus;
                getToUpdate.Members = aModel.Members;
                getToUpdate.DateUpdated = DateTime.Now;
                getToUpdate.EndTime = aModel.End;
                fcontext.Entry(getToUpdate).State = System.Data.Entity.EntityState.Modified;
                fcontext.SaveChanges();
                result = true;

            }
            catch (Exception ex)
            {
                result = false;
            }
            //});
            return result;
        }

    }
}
