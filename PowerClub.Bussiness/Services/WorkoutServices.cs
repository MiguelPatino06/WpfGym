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
//using PowerClub.DataAccess.Utils;

namespace PowerClub.Bussiness.Services
{
	public class WorkoutServices: BaseServices
	{
	   // private readonly IGenericFactory<Workout, int> fWorkoutFactoryAsync;
		GYMEntities context = new GYMEntities();
		public WorkoutServices()
		{
			//fWorkoutFactoryAsync = fGymUnitOfWork.CreateFactory<Workout, int>();
		}

		public int Add(WorkoutModel aModel)
		{
			int result = 0;
			//await Task.Run(() =>
			//{
				try
				{
					Workout aNew = new Workout
					{
						Code = aModel.CodigoWorkout,
						Name = aModel.Name,
						Description = aModel.Description,
						Active = true,
						Parent = aModel.Parent == 0? null: aModel.Parent,
						Color = aModel.Color,
						DateCreated = DateTime.Now
					};
					context.Workout.Add(aNew);
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

		public bool Update(WorkoutModel aModel)
		{
			bool result = false;
			//await Task.Run(() =>
			//{
				try
				{
					var getToUpdate = context.Workout.First(a => a.Id == aModel.Id);
					getToUpdate.Code = aModel.CodigoWorkout;
					getToUpdate.Name = aModel.Name;
					getToUpdate.Description = aModel.Description;
					getToUpdate.Active = aModel.Active;
				    getToUpdate.Parent = aModel.Parent == 0 ? null : aModel.Parent; //aModel.Parent;
					getToUpdate.Color = aModel.Color;
					getToUpdate.DateUpdated = aModel.DateUpdated;
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

		public WorkoutModel GetByID(int Id)
		{
			WorkoutModel workout = new WorkoutModel();
			List<Workout> listparent = new List<Workout>();
			try
			{
				var _workout = context.Workout.First(a => a.Id == Id);

				if (_workout != null)
				{
					workout.Id = _workout.Id;
					workout.CodigoWorkout = _workout.Code;
					workout.Name = _workout.Name;
					workout.Description = _workout.Description;
					workout.Parent = _workout.Parent;
					workout.Active = _workout.Active;
					workout.Color = _workout.Color;
					workout.DateCreated = _workout.DateCreated;
					workout.DateUpdated = _workout.DateUpdated;
					if (_workout.Parent != null)
					{
						var parents = context.Workout.Where(a=> a.Parent == _workout.Parent).Select(a => new WorkoutModel()                           
							{
								Id = a.Id,
								Name = a.Name,
								Description = a.Description,
								Parent = a.Parent,
								Active = a.Active,
								Color = a.Color,
								DateCreated = a.DateCreated,
								DateUpdated = a.DateUpdated
							});
						workout.Parents = parents.Any()? parents.ToList(): null;
					}                  
				}
			}
			catch (Exception ex)
			{
				workout = null;
			}
			return workout;
		}


		public WorkoutModel GetByCode(string code)
		{
			WorkoutModel workout = new WorkoutModel();
			List<Workout> listparent = new List<Workout>();
			try
			{
				var _workout = context.Workout.First(a => a.Code == code);

				if (_workout != null)
				{
					workout.Id = _workout.Id;
					workout.CodigoWorkout = _workout.Code;
					workout.Name = _workout.Name;
					workout.Description = _workout.Description;
					workout.Parent = _workout.Parent;
					workout.Active = _workout.Active;
					workout.Color = _workout.Color;
					workout.DateCreated = _workout.DateCreated;
					workout.DateUpdated = _workout.DateUpdated;
					if (_workout.Parent != null)
					{
						var parents = context.Workout.Where(a => a.Parent == _workout.Parent).Select(a => new WorkoutModel()
						{
							Id = a.Id,
							Name = a.Name,
							Description = a.Description,
							Parent = a.Parent,
							Active = a.Active,
							Color = a.Color,
							DateCreated = a.DateCreated,
							DateUpdated = a.DateUpdated
						});
						workout.Parents = parents.Any() ? parents.ToList() : null;
					}
				}
			}
			catch (Exception ex)
			{
				workout = null;
			}
			return workout;
		}

		public List<WorkoutModel> GetAll()
		{
			try
			{
				return context.Workout.Select(a=> new WorkoutModel
				{
					Id = a.Id,
					CodigoWorkout = a.Code,
					Name = a.Name,
					Description = a.Description,
					Parent = a.Parent,
					Active = a.Active,
					IsActive = (a.Active == true) ? "Si" : "No",
					Color = a.Color,
					DateCreated = a.DateCreated,
					DateUpdated = a.DateUpdated                   
				}).ToList();

			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public ObservableCollection<WorkoutModel> GetList()
		{
			var data = new List<WorkoutModel>();
			try
			{
				var _workout = GetAll();

				if (_workout != null)
				{
					foreach (var item in _workout)
					{
                        string _nameparent = null;

						if (item.Parent != null)
						{
							var result = GetByID((int) item.Parent);
						    if (result != null)
						        _nameparent = result.Name;
						}

						data.Add(new WorkoutModel()
						{
							Id = item.Id,
							CodigoWorkout = item.CodigoWorkout ?? string.Empty,
							Name = item.Name,
							Description = item.Description,
							Parent = item.Parent,
							Active = item.Active,
							IsActive = item.IsActive,
							Color = item.Color,
							DateCreated = item.DateCreated,
							DateUpdated = item.DateUpdated,
							NameParent = _nameparent
						});
					}
				}
  
				return new ObservableCollection<WorkoutModel>(data);

			}
			catch (Exception ex)
			{
				return new ObservableCollection<WorkoutModel>();
			}
		}

		public bool DeleteUpdate(int Id)
		{
			bool result = false;
			WorkoutModel aModel = new WorkoutModel();
			try
			{
				Workout workout = fcontext.Workout.Find(Id);
				fcontext.Workout.Remove(workout);
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

	 

	}
}
