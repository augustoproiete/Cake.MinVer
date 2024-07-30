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

using System.Collections.Generic;
using Cake.Core.IO;

namespace Cake.MinVer.Utils;

internal interface IEnvironmentProvider
{
    void SetOverrides(IDictionary<string, string> overrides);

    string GetEnvironmentVariable(string name, string defaultValue = null);

    TEnum? GetEnvironmentVariableAsEnum<TEnum>(string name, TEnum? defaultValue = null)
        where TEnum : struct;

    bool? GetEnvironmentVariableAsBool(string name, bool? defaultValue = null);

    FilePath GetEnvironmentVariableAsFilePath(string name, FilePath defaultValue = null);
}
