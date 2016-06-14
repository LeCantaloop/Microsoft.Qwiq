﻿using Microsoft.IE.Qwiq.Mapper.Attributes;
using Microsoft.IE.Qwiq.Mocks;

namespace Microsoft.IE.Qwiq.Relatives.Tests.Mocks
{
    [WorkItemType("Mock Issue")]
    public class MockParentIdIssue
    {
        public static readonly IWorkItemType CustomWorkItemType = new MockWorkItemType
        {
            Name = "Mock Issue"
        };

        public int ParentId { get; set; }
    }
}