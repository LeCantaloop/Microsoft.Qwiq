﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Tfs = Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Microsoft.IE.Qwiq
{
    /// <summary>
    /// Wrapper around the TFS WorkItemStore. This exists so that every agent doesn't need to reference
    /// all the TFS libraries.
    /// </summary>
    public class WorkItemStoreProxy : IWorkItemStore
    {
        private readonly TfsTeamProjectCollection _tfs;
        private readonly Tfs.WorkItemStore _workItemStore;

        // To do: stub out the following
        public ITfsTeamProjectCollection TeamProjectCollection
        {
            get { return new TfsTeamProjectCollectionProxy(_workItemStore.TeamProjectCollection); }
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tfs.Dispose();
            }
        }
        #endregion

        public WorkItemStoreProxy(Uri endpoint, TfsClientCredentials credentials)
        {
            _tfs = new TfsTeamProjectCollection(endpoint, credentials);
            _workItemStore = _tfs.GetService<Tfs.WorkItemStore>();
        }

        public IEnumerable<IWorkItemLinkInfo> QueryLinks(string wiql)
        {
            var query = new Tfs.Query(_workItemStore, wiql);
            return query.RunLinkQuery().Select(item => new WorkItemLinkInfoProxy(item));
        }

        public IEnumerable<IWorkItem> Query(string wiql)
        {
            return _workItemStore.Query(wiql).Cast<Tfs.WorkItem>().Select(item => new WorkItemProxy(item));
        }

        public IEnumerable<IWorkItem> Query(IEnumerable<int> ids)
        {
            const string wiql = "SELECT * FROM WorkItems WHERE [System.ID] IN ({0})";
            var query = string.Format(CultureInfo.InvariantCulture, wiql, string.Join(",", ids));

            return Query(query);
        }

        public IWorkItem Query(int id)
        {
            return Query(new[] { id }).SingleOrDefault();
        }

        public IWorkItem Create(string type, string projectName)
        {
            var wits = _workItemStore.Projects[projectName].WorkItemTypes;
            return new WorkItemProxy(new Tfs.WorkItem(wits[type]));
        }
    }
}
