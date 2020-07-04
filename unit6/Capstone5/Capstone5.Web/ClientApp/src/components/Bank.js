import React from 'react';
import AccountForm from './AccountForm';

export default () => {
    return (
        <AccountForm 
            currentBalance={10.12} 
            handleEvent={console.log}
        />
    );
};
