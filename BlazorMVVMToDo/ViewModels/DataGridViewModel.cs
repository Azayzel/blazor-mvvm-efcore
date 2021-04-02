using BlazorMVVMToDo.Data;
using BlazorMVVMToDo.Models;
using BlazorMVVMToDo.Context;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace BlazorMVVMToDo.ViewModels {

    public class DataGridViewModel : BaseViewModel, IDataGridViewModel {

        public JASContext Context { get; set; }

        public async Task Initialize(JASContext context) {
            Context = context;
            await FetchIssues();
        }

        private ObservableCollection<Issue> _data = new ObservableCollection<Issue>();
        public ObservableCollection<Issue> Data {
            get => _data;
            private set {
                SetValue(ref _data, value);
            }
        }

        private List<Issue> _issueList = new List<Issue>();
        public List<Issue> IsssueList {
            get => _issueList;
            private set {
                SetValue(ref _issueList, value);
            }

        }

        private Issue _issue = new Issue();
        public Issue issue {
            get => _issue;
            set {
                SetValue(ref _issue, value);
            }
        }

        public int DataItems {
            get {
                return IsssueList.Where(i => i.Done.Equals(false)).Count();
            }
        }


        public async Task FetchIssues() {

            IsBusy = true;
            
            IsssueList = await Context.Issues.ToListAsync();
            Debug.WriteLine(" Fetching Issues: " + IsssueList.Count());
            Data = new ObservableCollection<Issue>(IsssueList);
            OnPropertyChanged(nameof(Data));
            IsBusy = false;
           

        }

     

        public async void DeleteRandomIssue() {

        }
            //
          /// <summary>
            /// Creates a new user from a seeder if not supplied
            /// </summary>
            /// <param name="issue"></param>
            /// <returns></returns>
        public async void CreateIssue(GridCommandEventArgs args) {

            Issue issue = args.Item as Issue;

            if(issue == null) { return;  }

            IsBusy = true;

            try {
                await Context.Issues.AddAsync(issue);
                await Context.SaveChangesAsync();
                Data.Add(issue);
                OnPropertyChanged(nameof(Data));
                IsBusy = false;
            }
            catch (DBConcurrencyException err) { }
            finally { IsBusy = false; }
        }

        /// <summary>
        /// Deletes the suppplied param if present, from the database
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        public async void DeleteIssue(GridCommandEventArgs args) {

            IsBusy = true;
            Issue issue = args.Item as Issue;
            if (issue == null) {return;
            }
            try {
                Context.Issues.Remove(issue);
                await Context.SaveChangesAsync();
                Data.Remove(issue);
                OnPropertyChanged(nameof(Data));
            }
            catch (DBConcurrencyException err) {

            }
            finally {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Update the supplied param
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        public async void UpdateIssue(GridCommandEventArgs args) {
            IsBusy = true;
            Issue issue = args.Item as Issue;
            if (issue == null) {
                return;
            }
            try {
                ///Context.Entry(issue).State = EntityState.Modified;
                
                await Context.SaveChangesAsync();

                OnPropertyChanged(nameof(Data));
                IsBusy = false;
            }
            catch (DBConcurrencyException err) { }
            finally { IsBusy = false; }

        }

    }
}
