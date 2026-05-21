using Microsoft.EntityFrameworkCore;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Entities.Model;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CartItemRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<string> AddByIdCartItemAsync(Guid id, Guid userId)
        {
            var cart = await applicationDbContext.Cart.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
                return "Cart Not Found";


            var cartItem = await applicationDbContext.CartItems.FirstOrDefaultAsync(f => f.Id == id && f.CartId == cart.CartId && f.IsActive == IsActive.Active);
            if (cartItem == null)
                return "Cart Item Not Found";


            cartItem.Quantity += 1;
            cartItem.UpdatedAt = DateTime.UtcNow;
            cartItem.SubTotal = cartItem.Quantity * cartItem.Price;
            await applicationDbContext.SaveChangesAsync();
            return "Cart Item Quantity Increased by 1";
        }

        public async Task<CartResponseDto> AddToCart(AddCartItemDto addCartItemDto)
        {
            var product = await applicationDbContext.Product.FirstOrDefaultAsync(p => p.ProductId == addCartItemDto.ProductId && p.IsActive == IsActive.Active);
            if (product == null)
                return null;

            var cart = await applicationDbContext.Cart.FirstOrDefaultAsync(c => c.UserId == addCartItemDto.UserId );
            if(cart == null)
            {
                cart = new Cart
                {
                    UserId = addCartItemDto.UserId,
                    CreatedAt = DateTime.UtcNow,
                };
                await applicationDbContext.Cart.AddAsync(cart);
                await applicationDbContext.SaveChangesAsync();
            }
            
            var existingItem = await applicationDbContext.CartItems.FirstOrDefaultAsync(x => x.CartId == cart.CartId && x.ProductId == addCartItemDto.ProductId);
            
            if (existingItem != null)
            {
                existingItem.Quantity += 1;
                existingItem.SubTotal = existingItem.Price * existingItem.Quantity;
                existingItem.IsActive = IsActive.Active;
                existingItem.UpdatedAt = DateTime.UtcNow;
                existingItem.IsDeleted = false;
            }
            else
            {
                var cartItem = new CartItems
                {
                    CartId = cart.CartId,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Quantity = 1,
                    SubTotal = product.Price * 1,
                    IsActive = IsActive.Active,
                    IsDeleted = false
                };
                await applicationDbContext.CartItems.AddAsync(cartItem);
                existingItem = cartItem;
            }

            await applicationDbContext.SaveChangesAsync();
            return new CartResponseDto
            {
                UserId = addCartItemDto.UserId,
                ProductId = existingItem.ProductId,
                ProductName = existingItem.ProductName,
                Price = existingItem.Price,
                Quantity = existingItem.Quantity,
                SubTotal = existingItem.SubTotal,
                CreatedAt = existingItem.CreatedAt
            };

        }

        public async Task<string> DeleteAllCartItemAsync(Guid userId)
        {
            var cart = await applicationDbContext.Cart.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
                return "Cart Not Found";


            var cartItem = await applicationDbContext.CartItems.Where(c => c.CartId == cart.CartId).ToListAsync();
            if (cartItem == null)
                return "Cart Item Not Found";

            foreach(var item in cart.CartItems)
            {
                item.Quantity = 0;
                item.IsActive = IsActive.Inactive;
                item.UpdatedAt = DateTime.UtcNow;
                item.IsDeleted = true;
            }
            applicationDbContext.CartItems.RemoveRange(cartItem);
            await applicationDbContext.SaveChangesAsync();
            return "All Cart Items Deleted Successfully";
        }

        public async Task<string> DeleteByIdCartItemAsync(Guid Id, Guid userId)
        {
            var cart = await applicationDbContext.Cart.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
                return "Cart Not Found"; 


            var cartItem = await applicationDbContext.CartItems.FirstOrDefaultAsync(f => f.Id == Id && f.CartId == cart.CartId);
            if (cartItem == null)
                return "Cart Item Not Found";

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity -= 1;
                    cartItem.SubTotal = cartItem.Price * cartItem.Quantity;
                    cartItem.UpdatedAt = DateTime.UtcNow;

                    await applicationDbContext.SaveChangesAsync();
                    return "Cart Item Quantity Decreased by 1";

                }
                else if (cartItem.Quantity == 1)
                {
                    cartItem.Quantity -= 1;
                    cartItem.SubTotal = cartItem.Price * 0;
                    cartItem.IsActive = IsActive.Inactive;
                    cartItem.UpdatedAt = DateTime.UtcNow;
                    cartItem.IsDeleted = true;
                    applicationDbContext.CartItems.RemoveRange(cartItem);
                    await applicationDbContext.SaveChangesAsync();
                    return "Cart Item Deleted Successfully";
                }
                else
                {
                    cartItem.IsActive = IsActive.Inactive;

                    await applicationDbContext.SaveChangesAsync();
                    return "Cart Item Deleted Successfully";
                }
            }
            
            throw new NotImplementedException();
        }

        public async Task<CartWithItemsDto> GetAllCartItem(Guid userId)
        {
            var cart = await applicationDbContext.Cart.FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return new CartWithItemsDto();

            var cartItems = await applicationDbContext.CartItems.Where(f => f.CartId == cart.CartId && f.IsActive == IsActive.Active).Include(f => f.Product).ToListAsync();

            

            return new CartWithItemsDto
            {
                UserId = userId,
                CartId = cartItems.FirstOrDefault()?.CartId ?? Guid.Empty,

                Items = cartItems.Select(f => new CartItemDto
                {
                    Id = f.Id,
                    ProductName = f.ProductName,
                    Quantity = f.Quantity,
                    Price = f.Price,
                    SubTotal = f.SubTotal,
                    ImageUrl = f.Product.ImageUrl,
                    IsActive = f.IsActive,
                    CreatedAt = f.CreatedAt
                }).ToList()
            };
        }
    }
}
