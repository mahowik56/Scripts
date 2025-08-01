using System;

namespace log4net.Core
{
	public class LevelEvaluator : ITriggeringEventEvaluator
	{
		private Level m_threshold;

		public Level Threshold
		{
			get
			{
				return m_threshold;
			}
			set
			{
				m_threshold = value;
			}
		}

		public LevelEvaluator()
			: this(Level.Off)
		{
		}

		public LevelEvaluator(Level threshold)
		{
			if (threshold == null)
			{
				throw new ArgumentNullException("threshold");
			}
			m_threshold = threshold;
		}

		public bool IsTriggeringEvent(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			return loggingEvent.Level >= m_threshold;
		}
	}
}
