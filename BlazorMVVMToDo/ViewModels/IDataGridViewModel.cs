using BlazorMVVMToDo.Data;
using BlazorMVVMToDo.Models;
using BlazorMVVMToDo.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace BlazorMVVMToDo.ViewModels {

    public interface IDataGridViewModel {

        bool IsBusy { get; set; }
        int DataItems { get; }

        JASContext Context { get; set; }
        Issue issue { get; set; }
        List<Issue> IsssueList { get; }

        ObservableCollection<Issue> Data { get; }

        event PropertyChangedEventHandler PropertyChanged;

        Task Initialize(JASContext context);
        Task FetchIssues();

        void CreateIssue(GridCommandEventArgs args);

        void UpdateIssue(GridCommandEventArgs args);

        void DeleteIssue(GridCommandEventArgs args);

        void DeleteRandomIssue();
    }
}
