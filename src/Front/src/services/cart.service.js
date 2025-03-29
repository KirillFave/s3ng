import * as signalR from '@microsoft/signalr';

const URL = '/ws/carthub';

export const CartService = {
    connection: new signalR.HubConnectionBuilder()
        .withUrl(URL, {
            transport: signalR.HttpTransportType.WebSockets,
            withCredentials: false,
        })
        .withAutomaticReconnect()
        .build(),
  
    isSubscribed: false,

    init() {
        this.connection.start().catch(err => console.log('SignalR Connection Error: ', err));
    },
  
    onCartUpdate(callback) {
        if (!this.isSubscribed) {
            this.connection.on('ReceiveCartUpdate', (userId, cart) => {
            console.log(`Cart updated for user ${userId}:`, cart);
            callback(userId, cart);
            });
            this.isSubscribed = true;
        }
    },
  
    addToCart(userId, item) {
        const newAbortController = new AbortController();
        this.connection.invoke('AddToCart', userId, item, newAbortController)
            .catch(err => console.log('SignalR AddToCart Error: ', err));
    },
  
    removeFromCart(userId, productId) {
        const newAbortController = new AbortController();
        this.connection.invoke('RemoveFromCart', userId, productId, newAbortController)
            .catch(err => console.log('SignalR RemoveFromCart Error: ', err));
    },
  
    getCart(userId) {
        const newAbortController = new AbortController();
        this.connection.invoke('GetCart', userId, newAbortController)
            .catch(err => console.log('SignalR GetCart Error: ', err));
    },

    clearCart(userId) {
        const newAbortController = new AbortController();
        this.connection.invoke('ClearCart', userId, newAbortController)
            .catch(err => console.log('SignalR ClearCart Error: ', err));
    }
  };
  
  CartService.init();