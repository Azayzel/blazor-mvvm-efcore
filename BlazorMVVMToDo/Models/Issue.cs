using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorMVVMToDo.Models
{



  public class Issue
  {
    public Issue()
    {

      Done = false;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ClosedOn { get; set; }
    public string Description { get; set; }
    public IssueType Type { get; set; }
    public IssueSeverity? Severity { get; set; }


    [NotMapped]
    public bool Done { get; set; }
    [NotMapped]
    public List<string> Labels { get; set; }

    public bool IsOpen
    {
      get
      {
        return this.ClosedOn.HasValue;
      }
      set
      {
        if (!this.ClosedOn.HasValue)
        {
          this.ClosedOn = DateTime.Now;
        }
      }
    }
  }
}

