﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.Health.Fhir.Core.Features.TaskManagement;

namespace Microsoft.Health.Fhir.Core.UnitTests.Features.TaskManagement
{
    public class TestTask : ITask
    {
        private Func<Task<TaskResultData>> _executeFunc;
        private Action _cancelAction;
        private bool _isCancelling = false;

        public TestTask(Func<Task<TaskResultData>> executeFunc, Action cancelAction, string runId)
        {
            _executeFunc = executeFunc;
            _cancelAction = cancelAction;
            RunId = runId;
        }

        public string RunId { get; set; }

        public Task<TaskResultData> ExecuteAsync()
        {
            return _executeFunc();
        }

        public void Cancel()
        {
            _cancelAction?.Invoke();

            _isCancelling = true;
        }

        public bool IsCancelling()
        {
            return _isCancelling;
        }

        public void Dispose()
        {
        }
    }
}
