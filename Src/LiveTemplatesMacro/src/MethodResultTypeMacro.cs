/*
 * Copyright 2007-2011 JetBrains s.r.o.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Macros;
using JetBrains.Util;

namespace JetBrains.ReSharper.PowerToys.LiveTemplatesMacro
{
  [MacroDefinition("LiveTemplatesMacro.MethodResultTypeMacro",
    ShortDescription = "Result type of containing method",
    LongDescription = "Obtains the result type of the containing method it is used in")]
  public class MethodResultTypeMacro : IMacroDefinition
  {
    public string GetPlaceholder(IDocument document, IEnumerable<IMacroParameterValue> parameters)
    {
      return "a";
    }

    public ParameterInfo[] Parameters
    {
      get { return EmptyArray<ParameterInfo>.Instance; }
    }
  }
}