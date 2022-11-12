using System;
using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Data;
using tech_test_payment_api.Models;
using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Dto.Order;
using System.Net;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<Order> AddOrder(Order order)
        {
            if (order.Items.Count < 1) return BadRequest(new {
                msg = "Necessário adicionar ao menos 1 item para registar a venda."
            });

            _context.Orders.Add(order);
            _context.SaveChanges();

            return Ok(order);
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<List<Order>> GetById(int id)
        {
            var dbOrder = _context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.Seller)
                .Include(o => o.Items)
                .ToList();

            if (dbOrder == null) return NotFound();

            return Ok(dbOrder);
        }

        [HttpPut("UpadateOrder/")]
        public ActionResult UpdateStatus(UpdateOrderDto updateOrder)
        {
            var dbOrder = _context.Orders.FirstOrDefault(o => o.Id == updateOrder.Id);

            if (dbOrder == null) return NotFound();  
            
            if (!CheckStatusChange(updateOrder, dbOrder))
            {
                return BadRequest(new {
                    msg = "Status can only be changed in this manner:"
                    + " <Awaiting_Payment> to either <Payment_Approved> or <Cancelled>."
                    + " <Payment_Approved> to either <Shipping> or <Cancelled>."
                    + " <Shipping> to <Delivered>."
                });                
            }

            dbOrder.Status = updateOrder.Status;
                
            _context.SaveChanges();       

            var response = _context.Orders
                .Where(o => o.Id == updateOrder.Id)
                .Include(o => o.Seller)
                .Include(o => o.Items);

            return Ok(response);
        }

        private bool CheckStatusChange(UpdateOrderDto updateOrder, Order order)
        {   
            if (order.Status == EStatus.Awaiting_Payment)                                         
            {
                if (order.Status == EStatus.Payment_Approved ||
                updateOrder.Status == EStatus.Cancelled)
                {
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            else if (order.Status == EStatus.Payment_Approved)
            {
                if (updateOrder.Status == EStatus.Cancelled ||
                updateOrder.Status == EStatus.Shipping)
                {
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            else if (order.Status == EStatus.Shipping)
            {
                if (updateOrder.Status == EStatus.Delivered)
                {
                    return true;                    
                }
                else
                {
                    return false;
                }                    
            }
            else
            {
                return false;
            }
        }
    }
}