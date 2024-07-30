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

using Cake.Core.IO;

namespace Cake.MinVer.Tests.Support;

internal class MinVerGlobalToolFixture : MinVerToolFixtureBase<MinVerGlobalTool>, IMinVerGlobalTool
{
    public MinVerGlobalToolFixture(MinVerToolFixture _, MinVerToolContext context)
        : base(context)
    {
        _tool = new MinVerGlobalTool(_.FileSystem, _.Environment, ProcessRunner, _.Tools, _.Log);
        StandardOutput = MinVerToolOutputs.DefaultOutputForGlobalTool;
    }

    public override FilePath DefaultToolPath => GetDefaultToolPath("minver.exe");
}
