//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class WorkoutParentClassSchedule
    {
        public int IdBranchOficce { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public Nullable<int> IdParent { get; set; }
        public string ParentName { get; set; }
        public Nullable<int> Parent { get; set; }
    }
}
