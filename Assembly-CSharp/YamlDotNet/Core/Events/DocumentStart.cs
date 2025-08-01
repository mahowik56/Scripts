using System.Globalization;
using YamlDotNet.Core.Tokens;

namespace YamlDotNet.Core.Events
{
	public class DocumentStart : ParsingEvent
	{
		private readonly TagDirectiveCollection tags;

		private readonly VersionDirective version;

		private readonly bool isImplicit;

		public override int NestingIncrease
		{
			get
			{
				return 1;
			}
		}

		internal override EventType Type
		{
			get
			{
				return EventType.DocumentStart;
			}
		}

		public TagDirectiveCollection Tags
		{
			get
			{
				return tags;
			}
		}

		public VersionDirective Version
		{
			get
			{
				return version;
			}
		}

		public bool IsImplicit
		{
			get
			{
				return isImplicit;
			}
		}

		public DocumentStart(VersionDirective version, TagDirectiveCollection tags, bool isImplicit, Mark start, Mark end)
			: base(start, end)
		{
			this.version = version;
			this.tags = tags;
			this.isImplicit = isImplicit;
		}

		public DocumentStart(VersionDirective version, TagDirectiveCollection tags, bool isImplicit)
			: this(version, tags, isImplicit, Mark.Empty, Mark.Empty)
		{
		}

		public DocumentStart(Mark start, Mark end)
			: this(null, null, true, start, end)
		{
		}

		public DocumentStart()
			: this(null, null, true, Mark.Empty, Mark.Empty)
		{
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Document start [isImplicit = {0}]", isImplicit);
		}

		public override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
