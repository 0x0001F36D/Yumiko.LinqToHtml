﻿
namespace Yumiko.LinqToHtml.Tags.Item.Scopes
{
    using Infrastructure;
    using Interfaces;
    using System;

    public sealed class Scopes
    {
        private Scopes(Type type)
        {
            t = type;
        }
        private Type t;
        public ITag Generate(ITag parent)=> t.GetConstructor(new[] { typeof(ITag) })?.Invoke(new[] { parent }) as ITag;
        public readonly static Scopes Comment =new Scopes(typeof(Comment));
        public readonly static Scopes Doctype =new Scopes( typeof(Doctype));

        public readonly static Scopes A =new Scopes( typeof(A));
        public readonly static Scopes Abbr =new Scopes( typeof(Abbr));
        public readonly static Scopes Acronym =new Scopes( typeof(Acronym));
        public readonly static Scopes Address =new Scopes( typeof(Address));
        public readonly static Scopes Applet =new Scopes( typeof(Applet));
        public readonly static Scopes Area =new Scopes( typeof(Area));
        public readonly static Scopes Article =new Scopes( typeof(Article));
        public readonly static Scopes Aside =new Scopes( typeof(Aside));
        public readonly static Scopes Audio =new Scopes( typeof(Audio));
        public readonly static Scopes B =new Scopes( typeof(B));
        public readonly static Scopes Base =new Scopes( typeof(Base));
        public readonly static Scopes Basefont =new Scopes( typeof(Basefont));
        public readonly static Scopes Bdi =new Scopes( typeof(Bdi));
        public readonly static Scopes Bdo =new Scopes( typeof(Bdo));
        public readonly static Scopes Big =new Scopes( typeof(Big));
        public readonly static Scopes Blockquote =new Scopes( typeof(Blockquote));
        public readonly static Scopes Body =new Scopes( typeof(Body));
        public readonly static Scopes Br =new Scopes( typeof(Br));
        public readonly static Scopes Button =new Scopes( typeof(Button));
        public readonly static Scopes Canvas =new Scopes( typeof(Canvas));
        public readonly static Scopes Caption =new Scopes( typeof(Caption));
        public readonly static Scopes Center =new Scopes( typeof(Center));
        public readonly static Scopes Cite =new Scopes( typeof(Cite));
        public readonly static Scopes Code =new Scopes( typeof(Code));
        public readonly static Scopes Col =new Scopes( typeof(Col));
        public readonly static Scopes Colgroup =new Scopes( typeof(Colgroup));
        public readonly static Scopes Command =new Scopes( typeof(Command));
        public readonly static Scopes Datalist =new Scopes( typeof(Datalist));
        public readonly static Scopes Dd =new Scopes( typeof(Dd));
        public readonly static Scopes Del =new Scopes( typeof(Del));
        public readonly static Scopes Details =new Scopes( typeof(Details));
        public readonly static Scopes Dfn =new Scopes( typeof(Dfn));
        public readonly static Scopes Dialog =new Scopes( typeof(Dialog));
        public readonly static Scopes Dir =new Scopes( typeof(Dir));
        public readonly static Scopes Div =new Scopes( typeof(Div));
        public readonly static Scopes Dl =new Scopes( typeof(Dl));
        public readonly static Scopes Dt =new Scopes( typeof(Dt));
        public readonly static Scopes Em =new Scopes( typeof(Em));
        public readonly static Scopes Embed =new Scopes( typeof(Embed));
        public readonly static Scopes Fieldset =new Scopes( typeof(Fieldset));
        public readonly static Scopes Figcaption =new Scopes( typeof(Figcaption));
        public readonly static Scopes Figure =new Scopes( typeof(Figure));
        public readonly static Scopes Font =new Scopes( typeof(Font));
        public readonly static Scopes Footer =new Scopes( typeof(Footer));
        public readonly static Scopes Form =new Scopes( typeof(Form));
        public readonly static Scopes Frame =new Scopes( typeof(Frame));
        public readonly static Scopes Frameset =new Scopes( typeof(Frameset));
        public readonly static Scopes H1 =new Scopes( typeof(H1));
        public readonly static Scopes H2 =new Scopes( typeof(H2));
        public readonly static Scopes H3 =new Scopes( typeof(H3));
        public readonly static Scopes H4 =new Scopes( typeof(H4));
        public readonly static Scopes H5 =new Scopes( typeof(H5));
        public readonly static Scopes H6 =new Scopes( typeof(H6));
        public readonly static Scopes Head =new Scopes( typeof(Head));
        public readonly static Scopes Header =new Scopes( typeof(Header));
        public readonly static Scopes Hr =new Scopes( typeof(Hr));
        public readonly static Scopes Html =new Scopes( typeof(Html));
        public readonly static Scopes I =new Scopes( typeof(I));
        public readonly static Scopes Iframe =new Scopes( typeof(Iframe));
        public readonly static Scopes Img =new Scopes( typeof(Img));
        public readonly static Scopes Input =new Scopes( typeof(Input));
        public readonly static Scopes Ins =new Scopes( typeof(Ins));
        public readonly static Scopes Kbd =new Scopes( typeof(Kbd));
        public readonly static Scopes Keygen =new Scopes( typeof(Keygen));
        public readonly static Scopes Label =new Scopes( typeof(Label));
        public readonly static Scopes Legend =new Scopes( typeof(Legend));
        public readonly static Scopes Li =new Scopes( typeof(Li));
        public readonly static Scopes Link =new Scopes( typeof(Link));
        public readonly static Scopes Main =new Scopes( typeof(Main));
        public readonly static Scopes Map =new Scopes( typeof(Map));
        public readonly static Scopes Mark =new Scopes( typeof(Mark));
        public readonly static Scopes Menu =new Scopes( typeof(Menu));
        public readonly static Scopes Menuitem =new Scopes( typeof(Menuitem));
        public readonly static Scopes Meta =new Scopes( typeof(Meta));
        public readonly static Scopes Meter =new Scopes( typeof(Meter));
        public readonly static Scopes Nav =new Scopes( typeof(Nav));
        public readonly static Scopes Noframes =new Scopes( typeof(Noframes));
        public readonly static Scopes Noscript =new Scopes( typeof(Noscript));
        public readonly static Scopes Object =new Scopes( typeof(Object));
        public readonly static Scopes Ol =new Scopes( typeof(Ol));
        public readonly static Scopes Optgroup =new Scopes( typeof(Optgroup));
        public readonly static Scopes Option =new Scopes( typeof(Option));
        public readonly static Scopes Output =new Scopes( typeof(Output));
        public readonly static Scopes P =new Scopes( typeof(P));
        public readonly static Scopes Param =new Scopes( typeof(Param));
        public readonly static Scopes Pre =new Scopes( typeof(Pre));
        public readonly static Scopes Progress =new Scopes( typeof(Progress));
        public readonly static Scopes Q =new Scopes( typeof(Q));
        public readonly static Scopes Rp =new Scopes( typeof(Rp));
        public readonly static Scopes Rt =new Scopes( typeof(Rt));
        public readonly static Scopes Ruby =new Scopes( typeof(Ruby));
        public readonly static Scopes S =new Scopes( typeof(S));
        public readonly static Scopes Samp =new Scopes( typeof(Samp));
        public readonly static Scopes Script =new Scopes( typeof(Script));
        public readonly static Scopes Section =new Scopes( typeof(Section));
        public readonly static Scopes Select =new Scopes( typeof(Select));
        public readonly static Scopes Small =new Scopes( typeof(Small));
        public readonly static Scopes Source =new Scopes( typeof(Source));
        public readonly static Scopes Span =new Scopes( typeof(Span));
        public readonly static Scopes Strike =new Scopes( typeof(Strike));
        public readonly static Scopes Strong =new Scopes( typeof(Strong));
        public readonly static Scopes Style =new Scopes( typeof(Style));
        public readonly static Scopes Sub =new Scopes( typeof(Sub));
        public readonly static Scopes Summary =new Scopes( typeof(Summary));
        public readonly static Scopes Sup =new Scopes( typeof(Sup));
        public readonly static Scopes Table =new Scopes( typeof(Table));
        public readonly static Scopes Tbody =new Scopes( typeof(Tbody));
        public readonly static Scopes Td =new Scopes( typeof(Td));
        public readonly static Scopes Textarea =new Scopes( typeof(Textarea));
        public readonly static Scopes Tfoot =new Scopes( typeof(Tfoot));
        public readonly static Scopes Th =new Scopes( typeof(Th));
        public readonly static Scopes Thead =new Scopes( typeof(Thead));
        public readonly static Scopes Time =new Scopes( typeof(Time));
        public readonly static Scopes Title =new Scopes( typeof(Title));
        public readonly static Scopes Tr =new Scopes( typeof(Tr));
        public readonly static Scopes Track =new Scopes( typeof(Track));
        public readonly static Scopes Tt =new Scopes( typeof(Tt));
        public readonly static Scopes U =new Scopes( typeof(U));
        public readonly static Scopes Ul =new Scopes( typeof(Ul));
        public readonly static Scopes Var =new Scopes( typeof(Var));
        public readonly static Scopes Video =new Scopes( typeof(Video));
        public readonly static Scopes Wbr =new Scopes( typeof(Wbr));
    }
}
