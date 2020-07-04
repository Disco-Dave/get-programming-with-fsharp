import React from 'react';

export default ({currentBalance, handleEvent}) => {
    function preventDefault(e) {
        if (e && e.preventDefault) {
            e.preventDefault();
        }
    }

    const [amount, setAmount] = React.useState(0.00);

    const handleClick = (eventType) => () => {
        if (handleEvent && amount !== '') {
            handleEvent({
                event: eventType,
                amount,
            });
        }
        setAmount(0.00);
    };


    return (
        <form onSubmit={preventDefault}> 
            <h2>Bank Acount - Current balance ${ currentBalance }</h2>

            <div className='form-group'>
                <label htmlFor='amount'>Amount ($)</label>
                <input 
                    type='number' 
                    className='form-control' 
                    id='amount' 
                    value={amount}
                    onChange={e => setAmount(e.target.value)}
                />
            </div>

            <div className='btn-group'>
                <button 
                    type='button' 
                    className='btn btn-primary' 
                    onClick={handleClick('withdraw')}
                >
                    Withdraw
                </button>
                <button 
                    type='button' 
                    className='btn btn-secondary' 
                    onClick={handleClick('deposit')}
                >
                    Deposit
                </button>
            </div>
        </form>
    );
};
