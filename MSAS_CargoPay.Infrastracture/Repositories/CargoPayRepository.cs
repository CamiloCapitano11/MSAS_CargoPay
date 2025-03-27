using MSAS_CargoPay.Core.DTOs;
using MSAS_CargoPay.Core.Entities.V1._0;
using MSAS_CargoPay.Core.Interfaces;
using MSAS_CargoPay.Core.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MSAS_CargoPay.Core.DTOs.V1._0;
using Dapper;

namespace MSAS_CargoPay.Infrastracture.Repositories
{
    public class CargoPayRepository : ICargoPayRepository
    {
        private readonly string _connectionString;

        public CargoPayRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ResponseDto<CreateCard> CreateCard(CreateCardRequestDto requestDto, LogApi log)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new { requestDto.CardNumber, requestDto.InitialBalance };
                    log.Query = StoreProcedures.CreateCard;

                    var result = connection.Query<CreateCard>(StoreProcedures.CreateCard, parameters, commandType: CommandType.StoredProcedure);
                    return new ResponseDto<CreateCard>()
                    {
                        Status = Messages.StatusSuccess,
                        ResponseCode = (int)HttpStatusCode.OK,
                        ResponseMessage = result == null ? "No se puede crear." : Messages.StatusSuccess,
                        Data = (CreateCard)result,
                    };
                }
            }
            catch (Exception ex) when (ex.Data != null)
            {
                return new ResponseDto<CreateCard>
                {
                    Status = Messages.StatusError,
                    ResponseCode = (int)HttpStatusCode.BadRequest,
                    ResponseMessage = ex.Message,
                    Data = null
                };
            }
        }

        public ResponseDto<CardBalance> GetCardBalance(string CardNumber, LogApi log)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new { CardNumber };
                    log.Query = StoreProcedures.CreateCard;

                    var result = connection.Query<CardBalance>(StoreProcedures.GetCardBalance, parameters, commandType: CommandType.StoredProcedure);
                    return new ResponseDto<CardBalance>()
                    {
                        Status = Messages.StatusSuccess,
                        ResponseCode = (int)HttpStatusCode.OK,
                        ResponseMessage = result == null ? "No existe información" : Messages.StatusSuccess,
                        Data = (CardBalance)result,
                    };
                }
            }
            catch (Exception ex) when (ex.Data != null)
            {
                return new ResponseDto<CardBalance>
                {
                    Status = Messages.StatusError,
                    ResponseCode = (int)HttpStatusCode.BadRequest,
                    ResponseMessage = ex.Message,
                    Data = null
                };
            }
        }

        public ResponseDto<ProcessPayment> ProcessPayment(ProcessPaymentRequestDto requestDto, LogApi log)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new { requestDto.CardNumber, requestDto.Amount };
                    log.Query = StoreProcedures.CreateCard;

                    var result = connection.Query<ProcessPayment>(StoreProcedures.CreatePayment, parameters, commandType: CommandType.StoredProcedure);
                    return new ResponseDto<ProcessPayment>()
                    {
                        Status = Messages.StatusSuccess,
                        ResponseCode = (int)HttpStatusCode.OK,
                        ResponseMessage = result == null ? "No fue posible crear." : Messages.StatusSuccess,
                        Data = (ProcessPayment)result,
                    };
                }
            }
            catch (Exception ex) when (ex.Data != null)
            {
                return new ResponseDto<ProcessPayment>
                {
                    Status = Messages.StatusError,
                    ResponseCode = (int)HttpStatusCode.BadRequest,
                    ResponseMessage = ex.Message,
                    Data = null
                };
            }
        }
    }
}
