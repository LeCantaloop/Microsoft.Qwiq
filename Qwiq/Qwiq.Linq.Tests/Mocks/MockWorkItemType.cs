﻿using System;

namespace Microsoft.IE.Qwiq.Linq.Tests.Mocks
{
    public class MockWorkItemType : IWorkItemType
    {
        public string Description
        {
            get { throw new NotImplementedException(); }
        }

        public string Name { get; set; }

        public IWorkItem NewWorkItem()
        {
            throw new NotImplementedException();
        }
    }
}