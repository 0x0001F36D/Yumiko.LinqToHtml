
namespace Yumiko.LinqToHtml.Scope
{
    using System.Linq;
    using System.Collections.Generic;
    using Yumiko.LinqToHtml.Interfaces;
    using Yumiko.LinqToHtml.Tags.Item;
    using System;
    using Yumiko.LinqToHtml.Tags.Infrastructure;
    using Tags;

    public sealed class Scope
    {

        private Scope(Type type)
        { 
            this.type = type;
            this.args = new Dictionary<Type, object>(0);
        }
        private IDictionary<Type,object> args;
        private Type type;

        internal ITag Generate(ITag parent)
        {
            var tag = this
            .type
            .GetConstructor(new[]
            {
                typeof(ITag)
            }
            .Concat(args.Keys).ToArray())?
            .Invoke(new[]
            {
                parent
            }
            .Concat(args.Values).ToArray()) as ITag;
            var n = tag.TagName;
            var p = tag.ParentTag;
            var o = tag.GetFragments;
            return lt as ITag;
        }
        private Scope(Type type , string tagName, TagType tagType, bool ignoreCase)
        {
            this.type = type;
            this.args = new Dictionary<Type, object>
            {
                [typeof(string)] = tagName,
                [typeof(TagType)] = tagType,
                [typeof(bool)] = ignoreCase
            };
        }
        

        #region Tags
        public static Scope Custom(string tagName, TagType tagType, bool ignoreCase=true) => new Scope(typeof(Custom), tagName, tagType, ignoreCase);
        public readonly static Scope Comment = new Scope(typeof(Comment));
        public readonly static Scope Doctype = new Scope(typeof(Doctype));
        public readonly static Scope A = new Scope(typeof(A));
        public readonly static Scope Abbr = new Scope(typeof(Abbr));
        public readonly static Scope Acronym = new Scope(typeof(Acronym));
        public readonly static Scope Address = new Scope(typeof(Address));
        public readonly static Scope Applet = new Scope(typeof(Applet));
        public readonly static Scope Area = new Scope(typeof(Area));
        public readonly static Scope Article = new Scope(typeof(Article));
        public readonly static Scope Aside = new Scope(typeof(Aside));
        public readonly static Scope Audio = new Scope(typeof(Audio));
        public readonly static Scope B = new Scope(typeof(B));
        public readonly static Scope Base = new Scope(typeof(Base));
        public readonly static Scope Basefont = new Scope(typeof(Basefont));
        public readonly static Scope Bdi = new Scope(typeof(Bdi));
        public readonly static Scope Bdo = new Scope(typeof(Bdo));
        public readonly static Scope Big = new Scope(typeof(Big));
        public readonly static Scope Blockquote = new Scope(typeof(Blockquote));
        public readonly static Scope Body = new Scope(typeof(Body));
        public readonly static Scope Br = new Scope(typeof(Br));
        public readonly static Scope Button = new Scope(typeof(Button));
        public readonly static Scope Canvas = new Scope(typeof(Canvas));
        public readonly static Scope Caption = new Scope(typeof(Caption));
        public readonly static Scope Center = new Scope(typeof(Center));
        public readonly static Scope Cite = new Scope(typeof(Cite));
        public readonly static Scope Code = new Scope(typeof(Code));
        public readonly static Scope Col = new Scope(typeof(Col));
        public readonly static Scope Colgroup = new Scope(typeof(Colgroup));
        public readonly static Scope Command = new Scope(typeof(Command));
        public readonly static Scope Datalist = new Scope(typeof(Datalist));
        public readonly static Scope Dd = new Scope(typeof(Dd));
        public readonly static Scope Del = new Scope(typeof(Del));
        public readonly static Scope Details = new Scope(typeof(Details));
        public readonly static Scope Dfn = new Scope(typeof(Dfn));
        public readonly static Scope Dialog = new Scope(typeof(Dialog));
        public readonly static Scope Dir = new Scope(typeof(Dir));
        public readonly static Scope Div = new Scope(typeof(Div));
        public readonly static Scope Dl = new Scope(typeof(Dl));
        public readonly static Scope Dt = new Scope(typeof(Dt));
        public readonly static Scope Em = new Scope(typeof(Em));
        public readonly static Scope Embed = new Scope(typeof(Embed));
        public readonly static Scope Fieldset = new Scope(typeof(Fieldset));
        public readonly static Scope Figcaption = new Scope(typeof(Figcaption));
        public readonly static Scope Figure = new Scope(typeof(Figure));
        public readonly static Scope Font = new Scope(typeof(Font));
        public readonly static Scope Footer = new Scope(typeof(Footer));
        public readonly static Scope Form = new Scope(typeof(Form));
        public readonly static Scope Frame = new Scope(typeof(Frame));
        public readonly static Scope Frameset = new Scope(typeof(Frameset));
        public readonly static Scope H1 = new Scope(typeof(H1));
        public readonly static Scope H2 = new Scope(typeof(H2));
        public readonly static Scope H3 = new Scope(typeof(H3));
        public readonly static Scope H4 = new Scope(typeof(H4));
        public readonly static Scope H5 = new Scope(typeof(H5));
        public readonly static Scope H6 = new Scope(typeof(H6));
        public readonly static Scope Head = new Scope(typeof(Head));
        public readonly static Scope Header = new Scope(typeof(Header));
        public readonly static Scope Hr = new Scope(typeof(Hr));
        public readonly static Scope Html = new Scope(typeof(Html));
        public readonly static Scope I = new Scope(typeof(I));
        public readonly static Scope Iframe = new Scope(typeof(Iframe));
        public readonly static Scope Img = new Scope(typeof(Img));
        public readonly static Scope Input = new Scope(typeof(Input));
        public readonly static Scope Ins = new Scope(typeof(Ins));
        public readonly static Scope Kbd = new Scope(typeof(Kbd));
        public readonly static Scope Keygen = new Scope(typeof(Keygen));
        public readonly static Scope Label = new Scope(typeof(Label));
        public readonly static Scope Legend = new Scope(typeof(Legend));
        public readonly static Scope Li = new Scope(typeof(Li));
        public readonly static Scope Link = new Scope(typeof(Link));
        public readonly static Scope Main = new Scope(typeof(Main));
        public readonly static Scope Map = new Scope(typeof(Map));
        public readonly static Scope Mark = new Scope(typeof(Mark));
        public readonly static Scope Menu = new Scope(typeof(Menu));
        public readonly static Scope Menuitem = new Scope(typeof(Menuitem));
        public readonly static Scope Meta = new Scope(typeof(Meta));
        public readonly static Scope Meter = new Scope(typeof(Meter));
        public readonly static Scope Nav = new Scope(typeof(Nav));
        public readonly static Scope Noframes = new Scope(typeof(Noframes));
        public readonly static Scope Noscript = new Scope(typeof(Noscript));
        public readonly static Scope Object = new Scope(typeof(Tags.Item.Object));
        public readonly static Scope Ol = new Scope(typeof(Ol));
        public readonly static Scope Optgroup = new Scope(typeof(Optgroup));
        public readonly static Scope Option = new Scope(typeof(Option));
        public readonly static Scope Output = new Scope(typeof(Output));
        public readonly static Scope P = new Scope(typeof(P));
        public readonly static Scope Param = new Scope(typeof(Param));
        public readonly static Scope Pre = new Scope(typeof(Pre));
        public readonly static Scope Progress = new Scope(typeof(Progress));
        public readonly static Scope Q = new Scope(typeof(Q));
        public readonly static Scope Rp = new Scope(typeof(Rp));
        public readonly static Scope Rt = new Scope(typeof(Rt));
        public readonly static Scope Ruby = new Scope(typeof(Ruby));
        public readonly static Scope S = new Scope(typeof(S));
        public readonly static Scope Samp = new Scope(typeof(Samp));
        public readonly static Scope Script = new Scope(typeof(Script));
        public readonly static Scope Section = new Scope(typeof(Section));
        public readonly static Scope Select = new Scope(typeof(Select));
        public readonly static Scope Small = new Scope(typeof(Small));
        public readonly static Scope Source = new Scope(typeof(Source));
        public readonly static Scope Span = new Scope(typeof(Span));
        public readonly static Scope Strike = new Scope(typeof(Strike));
        public readonly static Scope Strong = new Scope(typeof(Strong));
        public readonly static Scope Style = new Scope(typeof(Style));
        public readonly static Scope Sub = new Scope(typeof(Sub));
        public readonly static Scope Summary = new Scope(typeof(Summary));
        public readonly static Scope Sup = new Scope(typeof(Sup));
        public readonly static Scope Table = new Scope(typeof(Table));
        public readonly static Scope Tbody = new Scope(typeof(Tbody));
        public readonly static Scope Td = new Scope(typeof(Td));
        public readonly static Scope Textarea = new Scope(typeof(Textarea));
        public readonly static Scope Tfoot = new Scope(typeof(Tfoot));
        public readonly static Scope Th = new Scope(typeof(Th));
        public readonly static Scope Thead = new Scope(typeof(Thead));
        public readonly static Scope Time = new Scope(typeof(Time));
        public readonly static Scope Title = new Scope(typeof(Title));
        public readonly static Scope Tr = new Scope(typeof(Tr));
        public readonly static Scope Track = new Scope(typeof(Track));
        public readonly static Scope Tt = new Scope(typeof(Tt));
        public readonly static Scope U = new Scope(typeof(U));
        public readonly static Scope Ul = new Scope(typeof(Ul));
        public readonly static Scope Var = new Scope(typeof(Var));
        public readonly static Scope Video = new Scope(typeof(Video));
        public readonly static Scope Wbr = new Scope(typeof(Wbr));
        #endregion
        

    }
}

