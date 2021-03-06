﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CIAPI.DTO
{
    public partial class ListOpenPositionsResponseDTO
    {
        public void ResolveMagicNumbers(MagicNumberResolver resolver)
        {

            if (this.OpenPositions != null)
            {
                foreach (var dto in this.OpenPositions)
                {
                    dto.ResolveMagicNumbers(resolver);
                }    
            }
            
        }
    }

    public partial class ApiOrderResponseDTO
    {
        public void ResolveMagicNumbers(MagicNumberResolver resolver)
        {
            
            this.StatusReason_Resolved = resolver.ResolveMagicNumber(MagicNumberKeys.ApiOrderResponseDTO_StatusReason, this.StatusReason);    
            this.Status_Resolved = resolver.ResolveMagicNumber(MagicNumberKeys.ApiOrderResponseDTO_StatusReason, this.StatusReason);
            if (this.OCO!=null)
            {
                this.OCO.ResolveMagicNumbers(resolver);    
            }



            if (this.IfDone != null)
            {
                foreach (ApiIfDoneResponseDTO apiIfDoneResponseDTO in this.IfDone)
                {
                    if (apiIfDoneResponseDTO.Limit != null)
                    {
                        apiIfDoneResponseDTO.Limit.ResolveMagicNumbers(resolver);    
                    }
                    if (apiIfDoneResponseDTO.Stop != null)
                    {
                        apiIfDoneResponseDTO.Stop.ResolveMagicNumbers(resolver);    
                    }
                    
                }    
            }
            
        }
    }
    public partial class ApiTradeOrderResponseDTO
    {
        public void ResolveMagicNumbers(MagicNumberResolver resolver)
        {
            this.StatusReason_Resolved = resolver.ResolveMagicNumber(MagicNumberKeys.ApiOrderResponseDTO_StatusReason, this.StatusReason);
            this.Status_Resolved = resolver.ResolveMagicNumber(MagicNumberKeys.ApiOrderResponseDTO_StatusReason, this.StatusReason);

            if (this.Orders != null)
            {
                foreach (ApiOrderResponseDTO order in this.Orders)
                {
                    order.ResolveMagicNumbers(resolver);
                }    
            }
            
        }
    }
    public partial class ApiActiveStopLimitOrderDTO
    {
        public void ResolveMagicNumbers(MagicNumberResolver resolver)
        {
            this.Applicability_Resolved = resolver.ResolveMagicNumber(MagicNumberKeys.ApiActiveStopLimitOrderDTO_Applicability, this.Applicability);

        }
    }

    public partial class ApiOpenPositionDTO
    {
        public void ResolveMagicNumbers(MagicNumberResolver resolver)
        {
            this.Status_Resolved = resolver.ResolveMagicNumber(MagicNumberKeys.ApiOpenPositionDTO_Status, this.Status);
        }
    }


}