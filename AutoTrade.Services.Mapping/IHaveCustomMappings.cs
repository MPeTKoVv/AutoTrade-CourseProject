namespace AutoTrade.Services.Mapping
{
	using AutoMapper;

	public interface IHaveCustomMappings
	{
		void CreateMappings(IProfileExpression configuration);
	}
}
