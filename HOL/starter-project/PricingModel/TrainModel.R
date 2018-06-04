# Create Sample Data ------------------------------------------------------
# 1k rows
# ProductName | eAge (18-50) | Gender (M,F) | PurchasePrice (0.25 - 1.25)
# CoconutWater| 24 | F | 1.00
# CoconutWater| 24 | M | 0.50

# Given Purchase Age + Gender + ProductName predict the PurchasePrice

ages <- c(18:50)
prices <- c(0.25, 0.35, 0.50, 0.75, 0.99, 1.00, 1.25)
genders <- factor(c("M", "F"))
products <- factor(c("coconut water", "soda", "water"))

distAges <- function(x) {
    if (x > 17 & x < 30) {
        rep(x, 10)
    } else {
        x
    }
}

generateExample <- function(index) {
    age <- sample(18:50, 1)
    if (17 < age && age < 30) {
        purchasePrice <- prices[max(sample(1:length(prices) - 1, 1), 1)]
        if (purchasePrice < 0.75) {
            productSelected <- products[sample(2:3, 1)]
        } else {
            productSelected <- products[sample(1:2, 1)]
        }
    }
        else {
            purchasePrice <- prices[max(sample(4:length(prices) - 1, 1), 1)]
            if (purchasePrice < 0.99) {
                productSelected <- products[sample(2:3, 1)]
            } else {
                productSelected <- products[sample(1:2, 1)]
            }
        }

    gender <- genders[sample(1:2, 1)]
    result <- list(age = age, purchasePrice = purchasePrice, gender = gender, productSelected = productSelected)
    return(result)
}

res <- lapply(1:1000, generateExample)

sampleData <- do.call(rbind.data.frame, res)
summary(sampleData)

# END Create Sample Data ------------------------------------------------------

# Build the Model ------------------------------------------------------

# TODO: 1. Export the sample data to a file   
# Save the sampleData objec to a file named "sampleData.RData"

# TODO: 2. Build a linear regression model to predict purchase price given age, gender and productSelect 
pricingModel <- rxLinMod(#Complete this#)

# TODO: 3. Export the trained model to a file named pricingModel.RData 
# Complete this line

# TODO: 4. Save one example of the sample data to serve as an input template, to a file called inputExample.RData
inputExample <- #Complete this
# Complete this line