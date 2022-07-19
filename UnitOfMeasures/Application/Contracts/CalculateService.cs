using System.Text.RegularExpressions;
using UnitOfMeasures.Application.Models.DTO;
using UnitOfMeasures.Application.Models.DTO.CoefficientUnitDTOs;
using UnitOfMeasures.Application.Models.DTO.FormulaUnitDTOs;
using UnitOfMeasures.Application.Utilities;
using UnitOfMeasures.Domain.Models;
using UnitOfMeasures.Infrastructure.Persistents.DBContext;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;
using UnitOfMeasures.Application.Models.Enums;

namespace UnitOfMeasures.Application.Contracts
{
    public class CalculateService : ICalculateService
    {
        MeasureDBContext _context;
        IMapper _mapper;
        public CalculateService(MeasureDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<BaseMeasurementUnitDetailDTO>?> GetMeasures()
        {
            var allBases = await _context.BaseMeasurementUnits.AsNoTracking().ToListAsync();
            return allBases?.Count > 0 ? _mapper.Map<List<BaseMeasurementUnitDetailDTO>>(allBases) : default;
        }

        public async Task<List<UnitsDTO>> GetUnitsByMeasureName(string measureName)
        {
            ArgumentNullException.ThrowIfNull(measureName, nameof(measureName));


            var result = await GetAllBaseUnits(measureName);
            if (result?.Count > 0)
            {
                var children = await GetChildrenByBaseMeasure(result.Select(r => r.ID).ToList());
                if (children?.Count > 0) result.AddRange(children);
            }

            return result;
        }



        private async Task<List<UnitsDTO>?> GetAllBaseUnits(string measureName)
        {
            var allBases = await _context.BaseMeasurementUnits.Where(b => b.MeasurementDimensionName.Equals(measureName)).AsNoTracking().ToListAsync();
            return allBases?.Count > 0 ? _mapper.Map<List<UnitsDTO>>(allBases) : default;
        }

        private async Task<List<UnitsDTO>?> GetChildrenByBaseMeasure(List<int> IDList)
        {
            ArgumentNullException.ThrowIfNull(IDList);
            var childeren = await _context.FormulaUnits.Include(c => c.ChildUnit).Where(c => IDList.Contains(c.ChildUnit.BaseMeasuremenID))
                .AsNoTracking().ToListAsync();
            var result = new List<UnitsDTO>();
            if (childeren?.Count > 0) result.AddRange(_mapper.Map<List<UnitsDTO>>(childeren));
            var others = await _context.CoefficientUnits.Include(c => c.ChildUnit).Where(c => IDList.Contains(c.ChildUnit.BaseMeasuremenID))
                .AsNoTracking().ToListAsync();
            if (others?.Count > 0) result.AddRange(_mapper.Map<List<UnitsDTO>>(others));
            return result;
        }

        public double ConvertUnits(ConvertDTO dto)
        {
            ArgumentNullException.ThrowIfNull(nameof(dto));

            if (dto.From.Type == UnitType.baseMeasure || dto.To.Type == UnitType.baseMeasure)
            {
                if (dto.From.Type == UnitType.baseMeasure)
                {
                    if (dto.To.Type == UnitType.coefficient)
                        return ConvertFromBaseMeasureToCoefficient(dto.To, dto.Value);
                    if (dto.To.Type == UnitType.formula)
                        return ConvertFromBaseMeasureToFormula(dto.To, dto.Value);
                }
                else
                {
                    if (dto.From.Type == UnitType.coefficient)
                        return ConvertFromCoefficientToBaseMeasure(dto.To, dto.Value);
                    if (dto.From.Type == UnitType.formula)
                        return ConvertFromFormulaToBaseMeasure(dto.To, dto.Value);
                }
            }else
            {
                if (dto.From.Type == UnitType.coefficient)
                {

                }
            }
            throw new NotImplementedException();
        }

        #region PrivateMethods
        private Tuple<ChildUnit, BaseMeasurementUnit> GetUnitByCode(string fromSymbol)
        {
            var baseModel = _context.BaseMeasurementUnits.Local.Where(b => b.Code.Equals(fromSymbol)).SingleOrDefault();
            if (baseModel == null)
            {

            }
            throw new NotImplementedException();
        }

        private double ConvertFromBaseMeasureToCoefficient(UnitsDTO coefficientDTO, float value)
        {
            var model = _context.CoefficientUnits.Where(c => c.Id.Equals(coefficientDTO.ID)).FirstOrDefault();
            return (value / model.Ratio);
        }
        private double ConvertFromCoefficientToBaseMeasure(UnitsDTO coefficientDTO, float value)
        {
            var model = _context.CoefficientUnits.Where(c => c.Id.Equals(coefficientDTO.ID)).FirstOrDefault();
            return (value * model.Ratio);
        }

        private double ConvertFromBaseMeasureToFormula(UnitsDTO dto, float value)
        {
            var model = _context.FormulaUnits.Where(c => c.Id.Equals(dto.ID)).FirstOrDefault();
            var formula = model.ConvertFromBaseFormula.Replace("a", value.ToString());
            return float.Parse(CalculateFormula(formula));
        }

        private double ConvertFromFormulaToBaseMeasure(UnitsDTO dto, float value)
        {
            var model = _context.FormulaUnits.Where(c => c.Id.Equals(dto.ID)).FirstOrDefault();
            var formula = model.ConvertToBaseFormula.Replace("a", value.ToString());
            return float.Parse(CalculateFormula(formula));
        }


        private string CalculateFormula(string value)
        {
            Regex r = new Regex(Patterns.ParenthesesPattern, RegexOptions.IgnoreCase);
            Match m = r.Match(value);
            if (m.Success)
            {
                Group group = m.Groups[m.Groups.Count - 1];
                if (group?.Captures?.Count > 0)
                {
                    string newF = group.Captures[0].Value;
                    return CalculateFormula(value.Replace($"({newF})", CalculateFormula(group.Captures[0].Value).ToString()));
                }

                else
                {
                    Regex regex = new Regex(Patterns.FolrmulaPattern);
                    if (regex.IsMatch(value))
                    {
                        if (!Regex.IsMatch(value, Patterns.FormulaParenthesesLessPattern))
                            return value;
                        if (Regex.IsMatch(value, Patterns.SimpleFormulaPattern))
                            return CalculateSimpleMath(value);


                        int mulIndex = value.IndexOf('*');
                        int divIndex = value.IndexOf('/');
                        int minIndex = (mulIndex != -1 && divIndex != -1) ? Math.Min(mulIndex, divIndex) : (mulIndex != -1 && divIndex == -1 ? mulIndex : divIndex);
                        if (minIndex > -1)
                        {
                            var xxxxxxxx = value.Substring(0, minIndex);
                            var xxxxxxxx2 = value.Substring(minIndex + 1);
                            var leftSide = GetLeftSide(value.Substring(0, minIndex));
                            var rightSide = GetRightSide(value.Substring(minIndex + 1));

                            string newF = new string($"{leftSide}{value.Substring(minIndex, 1)}{rightSide}");
                            return CalculateFormula(value.Replace($"{newF}", CalculateFormula(newF).ToString()));
                        }
                        else
                        {
                            int plusIndex = value.IndexOf('+');
                            int minusIndex = value.IndexOf('-');
                            int min = (plusIndex != -1 && minusIndex != -1) ? Math.Min(plusIndex, minusIndex) : (plusIndex != -1 && minusIndex == -1 ? plusIndex : minusIndex);
                            if (min > -1)
                            {
                                var leftSide = GetLeftSide(value.Substring(0, min));
                                var rightSide = GetRightSide(value.Substring(min + 1));
                                string newF = new string($"{leftSide}{value.Substring(min, 1)}{rightSide}");
                                return CalculateFormula(value.Replace($"{newF}", CalculateFormula(newF).ToString()));
                            }
                            else return string.Empty;
                        }

                    }
                    else
                        return string.Empty;

                }
            }
            else
                return null;
        }

        private object GetRightSide(string value)
        {
            string[] prefixes = { "+", "-" };
            bool startWithSign = prefixes.Any(prefix => value.StartsWith(prefix));
            if (startWithSign)
            {
                var sign = value.Substring(0, 1);
                var first2 = value.Substring(1).Split(new char[] { '*', '+', '-', '/' }).FirstOrDefault();
                return $"{sign}{first2}";
            }
            else
            {
                var first = value.Split(new char[] { '*', '+', '-', '/' }).FirstOrDefault();
                return first;
            }
        }

        private object GetLeftSide(string value)
        {
            string rightSide = string.Empty;
            var last = value.Split(new char[] { '*', '+', '-', '/' }).LastOrDefault();
            Regex regex1 = new Regex(pattern: $@"\s*([-+][-+]{last}\s*)");
            var match = regex1.Match(last);
            if (match.Success)
                rightSide = match.Value.Substring(1);
            else
            {
                Regex regex3 = new Regex(pattern: $@"\s*([-+]{last}\s*)");
                var match3 = regex3.Match(value);
                if (match3.Success)
                    rightSide = match3.Value.Substring(1);
                else
                {
                    Regex regex2 = new Regex(pattern: $@"\s*([-+]{value}\s*)");
                    var match2 = regex2.Match(value);
                    if (match2.Success)
                        rightSide = match2.Value;
                    else
                        return value;
                }
            }
            return rightSide;
        }

        private string CalculateSimpleMath(string value)
        {
            var rightSide = string.Empty;
            var filter = new Regex(Patterns.LeftSideFormulaPattern);
            var match = filter.Match(value);
            if (match.Success)
                rightSide = match.Value;
            var left = float.Parse(value.Substring(0, rightSide.Length - 1));
            var right = float.Parse(value.Substring(rightSide.Length));
            var opp = value.Substring(rightSide.Length - 1, 1);

            switch (opp)
            {
                case "*": return (left * right).ToString();
                case "-": return (left - right).ToString();
                case "+": return (left + right).ToString();
                case "/": return (left / right).ToString();
            };
            return string.Empty;
        }


        #endregion PrivateMethods


    }
}
