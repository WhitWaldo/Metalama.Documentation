﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Metalama.Documentation.DfmExtensions;

[JsonConverter( typeof(StringEnumConverter) )]
public enum SandboxFileKind
{
    [EnumMember( Value = "!none" )]
    None,

    [EnumMember( Value = "!incompatible" )]
    Incompatible,

    [EnumMember( Value = "t" )]
    TargetCode,

    [EnumMember( Value = "a" )]
    AspectCode,

    [EnumMember( Value = "e" )]
    ExtraCode
}