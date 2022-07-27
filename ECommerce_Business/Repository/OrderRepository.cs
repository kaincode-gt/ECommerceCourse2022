﻿using AutoMapper;
using ECommerce_Common;
using ECommerce_DataAccess;
using ECommerce_DataAccess.Data;
using ECommerce_DataAccess.ViewModel;
using ECommerce_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Business.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public OrderRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<OrderDTO> Create(OrderDTO objDTO)
    {
        try
        {
            var obj = _mapper.Map<OrderDTO, Order>(objDTO);
            _db.OrderHeaders.Add(obj.OrderHeader);
            await _db.SaveChangesAsync();

            foreach (var details in obj.OrderDetails)
            {
                details.OrderHeaderId = obj.OrderHeader.Id;
            }
            _db.OrderDetails.AddRange(obj.OrderDetails);
            await _db.SaveChangesAsync();

            return new OrderDTO()
            {
                OrderHeader = _mapper.Map<OrderHeader, OrderHeaderDTO>(obj.OrderHeader),
                OrderDetails = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDTO>>(obj.OrderDetails).ToList()
            };
        }
        catch (Exception ex)
        {
            throw new Exception();
        }
        return objDTO;
    }

    public async Task<int> Delete(int id)
    {
        var objHeader = await _db.OrderHeaders.FirstOrDefaultAsync(x => x.Id == id);
        if (objHeader != null)
        {
            IEnumerable<OrderDetail> objDetail = _db.OrderDetails.Where(x => x.OrderHeaderId == id);
            _db.OrderDetails.RemoveRange(objDetail);
            _db.OrderHeaders.Remove(objHeader);
            return await _db.SaveChangesAsync();
        }
        return 0;
    }

    public async Task<OrderDTO> Get(int id)
    {
        Order order = new()
        {
            OrderHeader = _db.OrderHeaders.FirstOrDefault(x => x.Id == id),
            OrderDetails = _db.OrderDetails.Where(x => x.OrderHeaderId == id)
        };
        if (order != null)
        {
            return _mapper.Map<Order, OrderDTO>(order);
        }
        return new OrderDTO();
    }

    public async Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null)
    {
        List<Order> OrderFromDb = new List<Order>();
        IEnumerable<OrderHeader> orderHeaderList = _db.OrderHeaders;
        IEnumerable<OrderDetail> orderDetailList = _db.OrderDetails;

        foreach (OrderHeader header in orderHeaderList)
        {
            Order order = new Order()
            {
                OrderHeader = header,
                OrderDetails = orderDetailList.Where(x => x.OrderHeaderId == header.Id)
            };
            OrderFromDb.Add(order);
        }

        //do some filtering

        return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(OrderFromDb);
    }

    public async Task<OrderHeaderDTO> MarkPaymentSuccessful(int id)
    {
        var data = await _db.OrderHeaders.FindAsync(id);
        if (data == null)
        {
            return new OrderHeaderDTO();
        }
        if (data.Status == SD.Status_Pending)
        {
            data.Status = SD.Status_Confirmed;
            await _db.SaveChangesAsync();
            return _mapper.Map<OrderHeader, OrderHeaderDTO>(data);
        }
        return new OrderHeaderDTO();
    }

    public async Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO objDTO)
    {
        if (objDTO != null)
        {
            var orderHeader = _mapper.Map<OrderHeaderDTO, OrderHeader>(objDTO);
            _db.OrderHeaders.Update(orderHeader);
            await _db.SaveChangesAsync();
            return _mapper.Map<OrderHeader, OrderHeaderDTO>(orderHeader);
        }
        return new OrderHeaderDTO();
    }

    public async Task<bool> UpdateOrderStatus(int orderId, string status)
    {
        var data = await _db.OrderHeaders.FindAsync(orderId);
        if (data == null)
        {
            return false;
        }

        data.Status = status;

        if (status == SD.Status_Shipped)
        {
            data.ShippingDate = DateTime.Now;
        }
        await _db.SaveChangesAsync();
        return true;
    }
}
