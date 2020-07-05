import React from 'react';
import AccountForm from './bank/AccountForm';
import CustomerForm from './bank/CustomerForm';

export default () => {
    const [account, setAccount] = React.useState(null);

    if (account) {
        return (
            <div>
                <AccountForm
                    currentBalance={account.balance} 
                    handleEvent={console.log}
                />

                <button className='btn btn-danger' onClick={_ => setAccount(null)}>
                    Logout
                </button>
            </div>
        );
    } else {
        return (
            <CustomerForm
                gotAccount={setAccount}
            />
        );
    }
};
