import React from 'react';

export default ({gotAccount}) => {
    function preventDefault(e) {
        if (e && e.preventDefault) {
            e.preventDefault();
        }
    }

    const [customer, setCustomer] = React.useState('');

    const handleClick = () => {
        if (gotAccount && customer !== '') {
            gotAccount({
                balance: 10.22,
            });
        }
    };


    return (
        <form onSubmit={preventDefault}> 
            <h2>Bank Account</h2>

            <div className='form-group'>
                <label htmlFor='customer'>Customer Name</label>
                <input 
                    type='test' 
                    className='form-control' 
                    id='customer' 
                    value={customer}
                    onChange={e => setCustomer(e.target.value)}
                />
            </div>

            <button 
                type='submit' 
                className='btn btn-primary' 
                onClick={handleClick}
            >
                Login
            </button>
        </form>
    );
};
