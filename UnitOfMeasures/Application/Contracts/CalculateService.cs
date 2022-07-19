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
using System.ComponentModel.DataAnnotations;
using UnitOfMeasures.Application.Models.Validations;

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

        public double ConvertUnits(string FromCode, string ToCode, [ValidDoubleValidator] double Value)
        {
            ArgumentNullException.ThrowIfNull(FromCode, "From");
            ArgumentNullException.ThrowIfNull(ToCode, "To");
            if (!(new ValidDoubleValidator().IsValid(Value)) || Value is double.NaN )
                throw new ArgumentException("Invalid Value.");

            UnitsDTO From = GetUnitFromCode(FromCode);
            UnitsDTO To = GetUnitFromCode(ToCode);

            if (From == null || To == null)
                throw new ArgumentException();

            if (!IsConvertValid(From, To))
                throw new InvalidCastException();


            if (!Enum.IsDefined(typeof(UnitTypeEnum), From.Type) || !Enum.IsDefined(typeof(UnitTypeEnum), To.Type))
                throw new ArgumentException("Invalid Unit Type.");

            if (From.Type.Equals(UnitTypeEnum.baseMeasure) && To.Type.Equals(UnitTypeEnum.baseMeasure))
                throw new ArgumentException("Cannot convart a Base Measure to another. Invalid Casting.");

            if (From.Type == UnitTypeEnum.baseMeasure || To.Type == UnitTypeEnum.baseMeasure)
            {
                if (From.Type == UnitTypeEnum.baseMeasure)
                {
                    if (To.Type == UnitTypeEnum.coefficient)
                        return ConvertFromBaseMeasureToCoefficient(To, Value);
                    if (To.Type == UnitTypeEnum.formula)
                        return ConvertFromBaseMeasureToFormula(To, Value);
                }
                else
                {
                    if (From.Type == UnitTypeEnum.coefficient)
                        return ConvertFromCoefficientToBaseMeasure(From, Value);
                    if (From.Type == UnitTypeEnum.formula)
                        return ConvertFromFormulaToBaseMeasure(From, Value);
                }
            }
            else
            {
                var baseCode = GetParentCode(FromCode);
                return ConvertUnits(baseCode, ToCode, ConvertUnits(FromCode, baseCode, Value));
            }
            return double.NaN;
        }

        #region PrivateMethods
        public string GetParentCode(string childCode)
        {
            if (string.IsNullOrEmpty(childCode) || string.IsNullOrWhiteSpace(childCode))
                throw new ArgumentNullException(nameof(childCode));

            var child = _context.ChildUnits.FirstOrDefault(c => c.Code.Equals(childCode));
            if (child == null)
                throw new InvalidOperationException();

            return _context.BaseMeasurementUnits.FirstOrDefault(c => c.Id.Equals(child.BaseMeasuremenID))?.Code ?? throw new InvalidOperationException();
        }

        public bool IsConvertValid(UnitsDTO From, UnitsDTO To)
        {
            if (From.Type == UnitTypeEnum.baseMeasure)
            {
                var toModel = _context.ChildUnits.Where(c => c.Id.Equals(To.ID)).SingleOrDefault();
                if (toModel != null)
                {
                    return toModel.BaseMeasuremenID.Equals(From.ID) ? true : throw new InvalidCastException();
                }
                else
                    throw new InvalidCastException();
            }
            else if (To.Type == UnitTypeEnum.baseMeasure)
            {
                var fromModel = _context.ChildUnits.Where(c => c.Id.Equals(From.ID)).SingleOrDefault();
                if (fromModel != null)
                {
                    return fromModel.BaseMeasuremenID.Equals(To.ID) ? true : throw new InvalidCastException();
                }
                else
                    throw new InvalidCastException();
            }
            else
            {
                var fromModel = _context.ChildUnits.Where(c => c.Id.Equals(From.ID)).SingleOrDefault();
                var toModel = _context.ChildUnits.Where(c => c.Id.Equals(To.ID)).SingleOrDefault();
                if (fromModel != null && toModel != null)
                {
                    return fromModel.BaseMeasuremenID.Equals(toModel.BaseMeasuremenID) ? true : throw new InvalidCastException();
                }
                else
                    throw new InvalidCastException();
            }
        }

        public UnitsDTO GetUnitFromCode(string code)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            var model = _context.BaseMeasurementUnits.FirstOrDefault(c => c.Code.Equals(code));
            if (model != null)
                return _mapper.Map<UnitsDTO>(model);
            else
            {
                var childModel = _context.CoefficientUnits.Include(c => c.ChildUnit).FirstOrDefault(c => c.ChildUnit.Code.Equals(code));
                if (childModel != null)
                {
                    return _mapper.Map<UnitsDTO>(childModel);
                }
                else
                {
                    var formulaModel = _context.FormulaUnits.Include(c => c.ChildUnit).FirstOrDefault(c => c.ChildUnit.Code.Equals(code));
                    if (formulaModel != null)
                        return _mapper.Map<UnitsDTO>(formulaModel);
                }
            }
            throw new InvalidOperationException();
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

        private double ConvertFromBaseMeasureToCoefficient(UnitsDTO coefficientDTO, double value)
        {
            var model = _context.CoefficientUnits.Where(c => c.Id.Equals(coefficientDTO.ID)).FirstOrDefault();
            if (model == null)
                throw new InvalidOperationException();
            return Math.Round((value / model.Ratio), 6);
        }
        private double ConvertFromCoefficientToBaseMeasure(UnitsDTO coefficientDTO, double value)
        {
            var model = _context.CoefficientUnits.Where(c => c.Id.Equals(coefficientDTO.ID)).FirstOrDefault();
            if (model == null)
                throw new InvalidOperationException();
            return Math.Round((value * model.Ratio), 6);
        }

        private double ConvertFromBaseMeasureToFormula(UnitsDTO dto, double value)
        {
            var model = _context.FormulaUnits.Where(c => c.Id.Equals(dto.ID)).FirstOrDefault();
            if (model == null)
                throw new InvalidOperationException();

            var formula = model.ConvertFromBaseFormula.Replace("a", value.ToString()).Replace(" ", "").Trim();
            return Math.Round(double.Parse(CalculateFormula(formula)), 6);

        }

        private double ConvertFromFormulaToBaseMeasure(UnitsDTO dto, double value)
        {
            var model = _context.FormulaUnits.Where(c => c.Id.Equals(dto.ID)).FirstOrDefault();
            if (model == null)
                throw new InvalidOperationException();

            var formula = model.ConvertToBaseFormula.Replace("a", value.ToString()).Replace(" ", "").Trim();
            var result = double.Parse(CalculateFormula(formula));
            return Math.Round(result, 6);
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
            Regex regex2 = new Regex(pattern: $@"\s*([-+]{value.Substring(1)}\s*)");
            var match2 = regex2.Match(value);
            if (match2.Success)
                return match2.Value;


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
                    return value;

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
