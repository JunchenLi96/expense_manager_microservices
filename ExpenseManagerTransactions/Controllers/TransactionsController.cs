using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseManagerTransactions.DomainModels;
using ExpenseManagerTransactions.RequestModels;
using ExpenseManagerTransactions.Services;
using ExpenseManagerTransactions.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagerTransactions.Configurations
{
    [ApiController]
    [Authorize]
    [Route("api/transaction")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            IEnumerable<Transaction> transactions = await _transactionService.GetAllTransactions(ClaimsExtensions.GetUserID(User));
            return Ok(transactions);
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<Transaction>> GetTransactionByID(string ID)
        {
            Transaction transaction = await _transactionService.GetTransactionByID(ID);
            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTransaction(TransactionRequest transactionRequest)
        {
            Transaction transactionToCreate = new Transaction
            {
                Description = transactionRequest.Description,
                CreatedAt = DateTime.UtcNow.ToString(),
                Type = transactionRequest.Type,
                Value = transactionRequest.Value,
                UserID = ClaimsExtensions.GetUserID(User)
            };
            Transaction transaction = await _transactionService.CreateNewTransaction(transactionToCreate);
            return Ok(transaction);
        }

        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdateTransactionByID(int ID, TransactionRequest transactionRequest)
        {
            Transaction transactionToUpdate = new Transaction
            {
                Description = transactionRequest.Description,
                UpdatedAt = DateTime.UtcNow.ToString(),
                Type = transactionRequest.Type,
                Value = transactionRequest.Value,
                ID = Convert.ToInt32(ID),
                UserID = ClaimsExtensions.GetUserID(User)
            };
            Transaction result = await _transactionService.UpdateTransaction(transactionToUpdate);
            if (result == null) return BadRequest("Could not find the target transaction");
            return Ok(result);
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteTransactionByID(int ID)
        {
            int result = await _transactionService.DeleteTransaction(ID, ClaimsExtensions.GetUserID(User));
            if (result == 0) return BadRequest("Could not delete the transaction");
            return Ok();
        }
    }
}
