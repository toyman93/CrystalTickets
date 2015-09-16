using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface IDamageable {

    // Remove health/durability
    void damage(int damage);

    // Add health/durability
    void heal(int health);

    // Add any code that should be called when the object is destroyed - e.g. set animator state to 'dead' if this is a character
    void destroy();
}
