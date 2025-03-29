export class OrderInfo {
    constructor(userGuid, orderItems, status, paymentType, shipAddress) {
      this.userGuid = userGuid;
      this.orderItems = orderItems;
      this.status = status;
      this.paymentType = paymentType;
      this.shipAddress = shipAddress;
    }
  }