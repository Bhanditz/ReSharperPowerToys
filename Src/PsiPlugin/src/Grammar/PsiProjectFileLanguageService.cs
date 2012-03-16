﻿using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.PsiPlugin.Lexer;
using JetBrains.Text;
using System.Drawing;
using JetBrains.UI;
using JetBrains.Util.Special;

namespace JetBrains.ReSharper.PsiPlugin.Grammar
{
  [ProjectFileType(typeof(PsiProjectFileType))]
  public class PsiProjectFileLanguageService : ProjectFileLanguageService
  {
    public PsiProjectFileLanguageService(PsiProjectFileType projectFileType)
      : base(projectFileType) { }

      public override ILexerFactory GetMixedLexerFactory(ISolution solution, IBuffer buffer, IPsiSourceFile sourceFile = null)
      {
          {
              return new PsiLexerFactory();
          }
      }

      protected override PsiLanguageType PsiLanguageType { get { return PsiLanguage.Instance; } }

      public override Image Icon
      {
          get { return ImageLoader.GetImage("symbols.psiFile.png", typeof (PsiProjectFileLanguageService).Assembly); }
      }
  }
}
