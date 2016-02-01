﻿namespace Atata
{
    public class FindByIdAttribute : TermFindAttribute
    {
        public FindByIdAttribute(TermMatch match)
            : base(match)
        {
        }

        public FindByIdAttribute(TermFormat format, TermMatch match = TermMatch.Inherit)
            : base(format, match)
        {
        }

        public FindByIdAttribute(string value, TermMatch match)
            : base(value, match)
        {
        }

        public FindByIdAttribute(params string[] values)
            : base(values)
        {
        }

        protected override TermFormat DefaultFormat
        {
            get { return TermFormat.Dashed; }
        }

        protected override TermMatch DefaultMatch
        {
            get { return TermMatch.Equals; }
        }

        public override IElementFindStrategy CreateStrategy(UIComponentMetadata metadata)
        {
            return new FindByIdStrategy();
        }
    }
}
