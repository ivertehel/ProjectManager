//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectManagerSite.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project_Projects
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> ParrentProject_Id { get; set; }
        public System.Guid ChildProject_Id { get; set; }
    
        public virtual Projects Projects { get; set; }
        public virtual Projects Projects1 { get; set; }
    }
}