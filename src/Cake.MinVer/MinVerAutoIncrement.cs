﻿#region Copyright 2020-2024 C. Augusto Proiete & Contributors
//
// Licensed under the MIT (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://opensource.org/licenses/MIT
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace Cake.MinVer;

/// <summary>
/// --auto-increment &lt;VERSION_PART&gt;
/// major, minor, or patch (default)
/// </summary>
public enum MinVerAutoIncrement
{
    /// <summary>
    /// --auto-increment major
    /// </summary>
    Major,

    /// <summary>
    /// --auto-increment minor
    /// </summary>
    Minor,

    /// <summary>
    /// --auto-increment patch
    /// </summary>
    Patch,
}
