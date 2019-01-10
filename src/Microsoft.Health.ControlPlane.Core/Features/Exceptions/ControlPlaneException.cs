﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.Health.ControlPlane.Core.Features.Exceptions
{
    public abstract class ControlPlaneException : Abstractions.Exceptions.MicrosoftHealthException
    {
        protected ControlPlaneException(string message, ICollection<string> issues = null)
            : base(message)
        {
            Issues = issues;
        }

        public ICollection<string> Issues { get; } = new List<string>();
    }
}