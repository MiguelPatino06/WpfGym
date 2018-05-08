﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PowerClub.DataAccess.Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GYMEntities : DbContext
    {
        public GYMEntities()
            : base("name=GYMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TrainerType> TrainerType { get; set; }
        public virtual DbSet<WorkoutParentClassSchedule> WorkoutParentClassSchedule { get; set; }
        public virtual DbSet<BranchOffice> BranchOffice { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Trainer> Trainer { get; set; }
        public virtual DbSet<ClassSchedule> ClassSchedule { get; set; }
        public virtual DbSet<ReviewClassSchedule> ReviewClassSchedule { get; set; }
        public virtual DbSet<Workout> Workout { get; set; }
    
        public virtual int GetClassScheduleByOfficeWorkouTrainer(Nullable<int> idBrancOffice, Nullable<int> idWorkout, Nullable<int> idTrainer)
        {
            var idBrancOfficeParameter = idBrancOffice.HasValue ?
                new ObjectParameter("IdBrancOffice", idBrancOffice) :
                new ObjectParameter("IdBrancOffice", typeof(int));
    
            var idWorkoutParameter = idWorkout.HasValue ?
                new ObjectParameter("IdWorkout", idWorkout) :
                new ObjectParameter("IdWorkout", typeof(int));
    
            var idTrainerParameter = idTrainer.HasValue ?
                new ObjectParameter("IdTrainer", idTrainer) :
                new ObjectParameter("IdTrainer", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetClassScheduleByOfficeWorkouTrainer", idBrancOfficeParameter, idWorkoutParameter, idTrainerParameter);
        }
    
        public virtual ObjectResult<GetClassScheduleByOfficeWorkouTrainer1_Result> GetClassScheduleByOfficeWorkouTrainer1(Nullable<int> idBrancOffice, Nullable<int> idWorkout, Nullable<int> idTrainer)
        {
            var idBrancOfficeParameter = idBrancOffice.HasValue ?
                new ObjectParameter("IdBrancOffice", idBrancOffice) :
                new ObjectParameter("IdBrancOffice", typeof(int));
    
            var idWorkoutParameter = idWorkout.HasValue ?
                new ObjectParameter("IdWorkout", idWorkout) :
                new ObjectParameter("IdWorkout", typeof(int));
    
            var idTrainerParameter = idTrainer.HasValue ?
                new ObjectParameter("IdTrainer", idTrainer) :
                new ObjectParameter("IdTrainer", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetClassScheduleByOfficeWorkouTrainer1_Result>("GetClassScheduleByOfficeWorkouTrainer1", idBrancOfficeParameter, idWorkoutParameter, idTrainerParameter);
        }
    }
}
