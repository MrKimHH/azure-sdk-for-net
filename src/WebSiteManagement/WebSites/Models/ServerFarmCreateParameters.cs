// 
// Copyright (c) Microsoft and contributors.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// 
// See the License for the specific language governing permissions and
// limitations under the License.
// 

// Warning: This code was generated by a tool.
// 
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Linq;
using Microsoft.WindowsAzure.Management.WebSites.Models;

namespace Microsoft.WindowsAzure.Management.WebSites.Models
{
    /// <summary>
    /// Parameters supplied to the Create Server Farm operation.
    /// </summary>
    public partial class ServerFarmCreateParameters
    {
        private int? _currentNumberOfWorkers;
        
        /// <summary>
        /// The current number of Virtual Machines (VMs) in the server farm.
        /// </summary>
        public int? CurrentNumberOfWorkers
        {
            get { return this._currentNumberOfWorkers; }
            set { this._currentNumberOfWorkers = value; }
        }
        
        private Microsoft.WindowsAzure.Management.WebSites.Models.ServerFarmWorkerSize? _currentWorkerSize;
        
        /// <summary>
        /// The current worker size Possible values are Small, Medium, or Large.
        /// </summary>
        public Microsoft.WindowsAzure.Management.WebSites.Models.ServerFarmWorkerSize? CurrentWorkerSize
        {
            get { return this._currentWorkerSize; }
            set { this._currentWorkerSize = value; }
        }
        
        private int _numberOfWorkers;
        
        /// <summary>
        /// The instance count, which is the number of virtual machines
        /// dedicated to the farm. Supported values are 1-10.
        /// </summary>
        public int NumberOfWorkers
        {
            get { return this._numberOfWorkers; }
            set { this._numberOfWorkers = value; }
        }
        
        private Microsoft.WindowsAzure.Management.WebSites.Models.ServerFarmStatus? _status;
        
        /// <summary>
        /// Server farm status. Possible values are Ready or Pending.
        /// </summary>
        public Microsoft.WindowsAzure.Management.WebSites.Models.ServerFarmStatus? Status
        {
            get { return this._status; }
            set { this._status = value; }
        }
        
        private ServerFarmWorkerSize _workerSize;
        
        /// <summary>
        /// The instance size. Possible values are Small, Medium, or Large.
        /// </summary>
        public ServerFarmWorkerSize WorkerSize
        {
            get { return this._workerSize; }
            set { this._workerSize = value; }
        }
        
        /// <summary>
        /// Initializes a new instance of the ServerFarmCreateParameters class.
        /// </summary>
        public ServerFarmCreateParameters()
        {
        }
    }
}
