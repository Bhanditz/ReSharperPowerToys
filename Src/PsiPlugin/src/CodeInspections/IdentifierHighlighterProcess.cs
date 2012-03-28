using JetBrains.Application.Settings;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.Stages;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.PsiPlugin.CodeInspections.Highlightings;
using JetBrains.ReSharper.PsiPlugin.Tree;
using JetBrains.ReSharper.PsiPlugin.Tree.Impl;

namespace JetBrains.ReSharper.PsiPlugin.CodeInspections
{
  internal class IdentifierHighlighterProcess : PsiIncrementalDaemonStageProcessBase
  {
    public IdentifierHighlighterProcess(IDaemonProcess daemonProcess, IContextBoundSettingsStore settingsStore)
      : base(daemonProcess, settingsStore)
    {
    }

    public override void VisitRuleDeclaredName(IRuleDeclaredName ruleDeclaredName, IHighlightingConsumer consumer)
    {
      var colorConstantRange = ruleDeclaredName.GetDocumentRange();
      AddHighLighting(colorConstantRange, ruleDeclaredName, consumer, new PsiRuleHighlighting(ruleDeclaredName));
      base.VisitRuleDeclaredName(ruleDeclaredName, consumer);
    }

    public override void VisitRuleName(IRuleName ruleName, IHighlightingConsumer consumer)
    {
      var colorConstantRange = ruleName.GetDocumentRange();

      var resolve = ruleName.RuleNameReference.Resolve();

      var isRuleResolved =  resolve.Result.DeclaredElement != null || (resolve.Result.Candidates.Count > 0);
      if (isRuleResolved)
        AddHighLighting(colorConstantRange, ruleName, consumer, new PsiRuleHighlighting(ruleName));
      else
        AddHighLighting(colorConstantRange, ruleName, consumer, new PsiUnresolvedReferenceHighlighting(ruleName));

      base.VisitRuleName(ruleName, consumer);
    }

    public override void VisitNode(ITreeNode element, IHighlightingConsumer consumer)
    {
      var colorConstantRange = element.GetDocumentRange();

      if ((element is ITokenNode) && ((ITokenNode)element).GetTokenType().IsWhitespace)
        return;

      var variableName = element as VariableName;
      if (variableName != null)
      {
        ResolveResultWithInfo resolve = variableName.Resolve();
        if ((resolve != null) && ((resolve.Result.DeclaredElement != null) || (resolve.Result.Candidates.Count > 0)))
        {
          AddHighLighting(colorConstantRange, element, consumer, new PsiVariableHighlighting(element));
        }  else
        {
          AddHighLighting(colorConstantRange, element, consumer, new PsiUnresolvedReferenceHighlighting(element));
          return;
        }    
      }
      var pathName = element as PathName;
      if (pathName != null)
      {
        ResolveResultWithInfo resolve = pathName.Resolve();
        if ((resolve != null) && ((resolve.Result.DeclaredElement != null) || (resolve.Result.Candidates.Count > 0)))
        {
          AddHighLighting(colorConstantRange, element, consumer, new PsiRuleHighlighting(element));
        }
        else
        {
          AddHighLighting(colorConstantRange, element, consumer, new PsiUnresolvedReferenceHighlighting(element));
        }
      }
    }

    private void AddHighLighting(DocumentRange range, ITreeNode element, IHighlightingConsumer consumer, IHighlighting highlighting)
    {
      var info = new HighlightingInfo(range, highlighting, new Severity?());
      var file = element.GetContainingFile();
      if (file != null)
        consumer.AddHighlighting(info.Highlighting, file);

    }
  }
}