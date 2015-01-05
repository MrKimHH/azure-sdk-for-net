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
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Azure.Management.Resources.Models;

namespace Microsoft.Azure.Management.Resources
{
    /// <summary>
    /// Operations for managing tags.
    /// </summary>
    public partial interface ITagOperations
    {
        /// <summary>
        /// Create a subscription resource tag.
        /// </summary>
        /// <param name='tagName'>
        /// The name of the tag.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// Tag information.
        /// </returns>
        Task<TagCreateResult> CreateOrUpdateAsync(string tagName, CancellationToken cancellationToken);
        
        /// <summary>
        /// Create a subscription resource tag value.
        /// </summary>
        /// <param name='tagName'>
        /// The name of the tag.
        /// </param>
        /// <param name='tagValue'>
        /// The value of the tag.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// Tag information.
        /// </returns>
        Task<TagCreateValueResult> CreateOrUpdateValueAsync(string tagName, string tagValue, CancellationToken cancellationToken);
        
        /// <summary>
        /// Delete a subscription resource tag.
        /// </summary>
        /// <param name='tagName'>
        /// The name of the tag.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A standard service response including an HTTP status code and
        /// request ID.
        /// </returns>
        Task<AzureOperationResponse> DeleteAsync(string tagName, CancellationToken cancellationToken);
        
        /// <summary>
        /// Delete a subscription resource tag value.
        /// </summary>
        /// <param name='tagName'>
        /// The name of the tag.
        /// </param>
        /// <param name='tagValue'>
        /// The value of the tag.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// A standard service response including an HTTP status code and
        /// request ID.
        /// </returns>
        Task<AzureOperationResponse> DeleteValueAsync(string tagName, string tagValue, CancellationToken cancellationToken);
        
        /// <summary>
        /// Get a list of subscription resource tags.
        /// </summary>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// List of subscription tags.
        /// </returns>
        Task<TagsListResult> ListAsync(CancellationToken cancellationToken);
        
        /// <summary>
        /// Get a list of tags under a subscription.
        /// </summary>
        /// <param name='nextLink'>
        /// NextLink from the previous successful call to List operation.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// List of subscription tags.
        /// </returns>
        Task<TagsListResult> ListNextAsync(string nextLink, CancellationToken cancellationToken);
    }
}
